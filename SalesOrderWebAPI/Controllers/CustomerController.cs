using Microsoft.AspNetCore.Mvc;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Repository;


namespace SalesOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customer;
        private readonly ILogger<CustomerRepository> _logger;

        public CustomerController(ICustomerRepository customer, ILogger<CustomerRepository> logger)
        {
            _customer = customer;
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public async Task<List<CustomerDto>> GetAll()
        {
            this._logger.LogInformation("|Log ||Testing");
            return await this._customer.Getall();

        }
        [HttpGet("GetByCode")]
        public async Task<CustomerDto> GetByCode(string Code)
        {
            return await this._customer.Getbycode(Code);

        }
    }
}
