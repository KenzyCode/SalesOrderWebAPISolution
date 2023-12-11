using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.DTO.UserResponseDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Repository
{

    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly SalesOrderDbContext _dBContext;
        private readonly IMapper _mapper;
        private readonly ILogger<InvoiceRepository> _logger;

        public InvoiceRepository(SalesOrderDbContext DBContext, IMapper mapper, ILogger<InvoiceRepository> _logger)
        {
            _dBContext = DBContext;
            _mapper = mapper;
            _logger = _logger;
        }

        public async Task<List<InvoiceHeader>> GetAllInvoiceHeader()
        {
            var _data = await _dBContext.TblSalesHeaders.ToListAsync();
            if (_data != null && _data.Count > 0)
            {
                return _mapper.Map<List<TblSalesHeader>, List<InvoiceHeader>>(_data);
            }
            return new List<InvoiceHeader>();
        }

        public async Task<InvoiceHeader> GetAllInvoiceHeaderbyCode(string invoiceno)
        {
            var _data = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceno);
            if (_data != null)
            {
                return _mapper.Map<TblSalesHeader, InvoiceHeader>(_data);
            }
            return new InvoiceHeader();
        }

        public async Task<List<InvoiceDetails>> GetAllInvoiceDetailbyCode(string invoiceno)
        {
            var _data = await _dBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceno).ToListAsync();
            if (_data != null && _data.Count > 0)
            {
                return _mapper.Map<List<TblSalesProductInfo>, List<InvoiceDetails>>(_data);
            }
            return new List<InvoiceDetails>();
        }

        public async Task<ResponseType> Save(InvoiceInput invoiceEntity)
        {
            string Result = string.Empty;
            int processcount = 0;
            var response = new ResponseType();
            if (invoiceEntity != null)
            {
                using (var dbtransaction = await _dBContext.Database.BeginTransactionAsync())
                {

                    if (invoiceEntity != null)
                        Result = await this.SaveHeader(invoiceEntity);

                    if (!string.IsNullOrEmpty(Result) && (invoiceEntity.details != null && invoiceEntity.details.Count > 0))
                    {
                        invoiceEntity.details.ForEach(item =>
                        {
                            bool saveresult = this.SaveDetail(item, invoiceEntity.CreateUser, invoiceEntity.InvoiceNo).Result;
                            if (saveresult)
                            {
                                processcount++;
                            }
                        });

                        if (invoiceEntity.details.Count == processcount)
                        {
                            await _dBContext.SaveChangesAsync();
                            await dbtransaction.CommitAsync();
                            response.Result = "pass";
                            response.KyValue = Result;
                        }
                        else
                        {
                            await dbtransaction.RollbackAsync();
                            response.Result = "faill";
                            response.Result = string.Empty;
                        }
                    }
                    else
                    {
                        response.Result = "faill";
                        response.Result = string.Empty;
                    }

                    

                };
            }
            else
            {
                return new ResponseType();
            }
            return response;

        }

        private async Task<string> SaveHeader(InvoiceInput invoiceHeader)
        {
            string Results = string.Empty;

            try
            {
                TblSalesHeader _header = _mapper.Map<InvoiceInput, TblSalesHeader>(invoiceHeader);
                _header.InvoiceDate = DateTime.Now;
                var header = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceHeader.InvoiceNo);

                if (header != null)
                {
                    header.CustomerId = invoiceHeader.CustomerId;
                    header.CustomerName = invoiceHeader.CustomerName;
                    header.DeliveryAddress = invoiceHeader.DeliveryAddress;
                    header.Total = invoiceHeader.Total;
                    header.Remarks = invoiceHeader.Remarks;
                    header.Tax = invoiceHeader.Tax;
                    header.NetTotal = invoiceHeader.NetTotal;
                    header.ModifyUser = invoiceHeader.CreateUser;
                    header.ModifyDate = DateTime.Now;

                    var _detdata = await _dBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceHeader.InvoiceNo).ToListAsync();
                    if (_detdata != null && _detdata.Count > 0)
                    {
                        _dBContext.TblSalesProductInfos.RemoveRange(_detdata);
                    }
                }
                else
                {
                    await _dBContext.TblSalesHeaders.AddAsync(_header);
                }
                Results = invoiceHeader.InvoiceNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Results;
        }
        private async Task<bool> SaveDetail(InvoiceDetails InvoiceDetails, string User, string InvoiceNo)
        {
            try
            {
                TblSalesProductInfo _detail = _mapper.Map<InvoiceDetails, TblSalesProductInfo>(InvoiceDetails);
                _detail.CreateDate = DateTime.Now;
                _detail.CreateUser = User;
                _detail.InvoiceNo = InvoiceNo;
                await _dBContext.TblSalesProductInfos.AddAsync(_detail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseType> Remove(string invoiceno)
        {
            try
            {
                using (var dbtransaction = await _dBContext.Database.BeginTransactionAsync())
                {
                    var _data = await _dBContext.TblSalesHeaders.FirstOrDefaultAsync(item => item.InvoiceNo == invoiceno);
                    if (_data != null)
                    {
                        _dBContext.TblSalesHeaders.Remove(_data);
                    }

                    var _detdata = await _dBContext.TblSalesProductInfos.Where(item => item.InvoiceNo == invoiceno).ToListAsync();
                    if (_detdata != null && _detdata.Count > 0)
                    {
                        _dBContext.TblSalesProductInfos.RemoveRange(_detdata);
                    }
                    await _dBContext.SaveChangesAsync();
                    await dbtransaction.CommitAsync();
                }
                return new ResponseType() { Result = "pass", KyValue = invoiceno };
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        
    }
}


