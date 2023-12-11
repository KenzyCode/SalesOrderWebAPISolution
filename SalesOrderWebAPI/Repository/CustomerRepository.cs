using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly SalesOrderDbContext _dBContext;
        private readonly IMapper _mapper;

        public CustomerRepository(SalesOrderDbContext DBContext, IMapper mapper)
        {
            _dBContext = DBContext;
            _mapper = mapper;
        }

        public async Task<List<CustomerDto>> Getall()
        {
            var customerdata = await _dBContext.TblCustomers.ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                // we need use automapper

                return _mapper.Map<List<TblCustomer>, List<CustomerDto>>(customerdata);
            }
            return new List<CustomerDto>();

        }

        public async Task<CustomerDto> Getbycode(string code)
        {
            int c = Convert.ToInt32(code);
            var customerdata = await _dBContext.TblCustomers.FirstOrDefaultAsync(item => item.Code == code);
            if (customerdata != null)
            {
                return _mapper.Map<TblCustomer, CustomerDto>(customerdata);
            }
            return new CustomerDto();

        }


    }
}

