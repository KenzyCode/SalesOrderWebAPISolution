using SalesOrderWebAPI.DTO.UserDto;

namespace SalesOrderWebAPI.IRepository
{
    public interface IMasterRepository
    {
        Task<List<VariantDto>> GetAllVariant(string variantType);
        Task<List<CategoryDto>> GetCategory();
    }
}
