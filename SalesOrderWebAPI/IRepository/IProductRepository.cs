using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.DTO.UserResponseDto;

namespace SalesOrderWebAPI.IRepository
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> Getall();
        Task<ProductDto> Getbycode(string code);
        Task<List<ProductDto>> Getbycategory(int Category);

        Task<ResponseType> SaveProduct(ProductDto product);
    }
}
