using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.DTO.UserResponseDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Repository
{

    public class ProductRepository : IProductRepository
    {
        private readonly SalesOrderDbContext _dBContext;
        private readonly IMapper _mapper;

        public ProductRepository(SalesOrderDbContext DBContext, IMapper mapper)
        {
            _dBContext = DBContext;
            _mapper = mapper;
        }

        public async Task<List<ProductDto>> Getall()
        {
            var customerdata = await _dBContext.TblProducts.ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                // we need use automapper

                return _mapper.Map<List<TblProduct>, List<ProductDto>>(customerdata);
            }
            return new List<ProductDto>();

        }

        public async Task<ProductDto> Getbycode(string code)
        {
            var customerdata = await _dBContext.TblProducts.FirstOrDefaultAsync(item => item.Code == code);
            if (customerdata != null)
            {
                var _proddata = _mapper.Map<TblProduct, ProductDto>(customerdata);
                if (_proddata != null)
                {
                    _proddata.Variants = GetVarintbyProduct(code).Result;
                }
                return _proddata;
            }
            return new ProductDto();

        }

        public async Task<List<ProductVariantDto>> GetVarintbyProduct(string productcode)
        {
            var customerdata = await _dBContext.TblProductvarinats.Where(item => item.ProductCode == productcode).ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                return _mapper.Map<List<TblProductvarinat>, List<ProductVariantDto>>(customerdata);
            }
            return new List<ProductVariantDto>();

        }

        public async Task<List<ProductDto>> Getbycategory(int Category)
        {
            var customerdata = await _dBContext.TblProducts.Where(item => item.Category == Category).ToListAsync();
            if (customerdata != null)
            {
                return _mapper.Map<List<TblProduct>, List<ProductDto>>(customerdata);
            }
            return new List<ProductDto>();

        }

        public async Task<ResponseType> SaveProduct(ProductDto product)
        {
            try
            {
                string Result = string.Empty;
                int processcount = 0;

                if (product != null)
                {
                    using (var dbtransaction = await _dBContext.Database.BeginTransactionAsync())
                    {
                        //check exist product
                        var _product = await _dBContext.TblProducts.FirstOrDefaultAsync(item => item.Code == product.Code);
                        if (_product != null)
                        {
                            // update here
                            _product.Name = product.Name;
                            _product.Category = product.Category;
                            _product.Price = product.Price;
                            await _dBContext.SaveChangesAsync();

                        }
                        else
                        {
                            // create new record
                            var _newproduct = new TblProduct()
                            {
                                Code = product.Code,
                                Name = product.Name,
                                Price = product.Price,
                                Category = product.Category,
                            };
                            await _dBContext.TblProducts.AddAsync(_newproduct);
                            await _dBContext.SaveChangesAsync();
                        }
                        if (product.Variants != null && product.Variants.Count > 0)
                        {
                            product.Variants.ForEach(item =>
                            {
                                var _resp = SaveProductVariant(item, product.Code);
                                if (_resp.Result)
                                {
                                    processcount++;
                                }
                            });
                            if (processcount == product.Variants.Count)
                            {
                                await _dBContext.SaveChangesAsync();
                                await dbtransaction.CommitAsync();
                                return new ResponseType() { KyValue = product.Code, Result = "pass" };
                            }
                            else
                            {
                                await dbtransaction.RollbackAsync();
                            }
                        }
                    }


                }
                else
                {

                }
            }


            catch (Exception ex)
            {
                throw ex;
            }

            return new ResponseType() { KyValue = string.Empty, Result = "fail" };
        }

        private async Task<bool> SaveProductVariant(ProductVariantDto _variant, string ProductCode)
        {
            bool Result = false;

            try
            {
                var _existdata = await _dBContext.TblProductvarinats.FirstOrDefaultAsync(item => item.Id == _variant.Id);
                if (_existdata != null)
                {

                    _existdata.ColorCode = _variant.ColorCode;
                    _existdata.SizeCode = _variant.SizeCode;
                    _existdata.ProductCode = _variant.ProductCode;
                    _existdata.Price = _variant.Price;
                    _existdata.Remarks = _variant.Remarks;
                }
                else
                {
                    var _newrecord = new TblProductvarinat()
                    {
                        ColorCode = _variant.ColorCode,
                        SizeCode = _variant.SizeCode,
                        Price = _variant.Price,
                        ProductCode = ProductCode,
                        Remarks = _variant.Remarks,
                        IsActive = true
                    };
                    await _dBContext.TblProductvarinats.AddAsync(_newrecord);
                }
                Result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Result;
        }


    }
}

