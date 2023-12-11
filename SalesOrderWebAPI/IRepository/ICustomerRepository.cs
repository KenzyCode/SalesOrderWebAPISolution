using SalesOrderWebAPI.DTO.UserDto;

namespace SalesOrderWebAPI.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerDto>> Getall();
        Task<CustomerDto> Getbycode(string code);
    }
}
