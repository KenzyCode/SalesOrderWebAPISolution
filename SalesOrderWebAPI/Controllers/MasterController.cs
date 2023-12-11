using Microsoft.AspNetCore.Mvc;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Repository;

namespace SalesOrderWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly IMasterRepository _master;
        private readonly ILogger<MasterRepository> _logger;

        public MasterController(IMasterRepository master, ILogger<MasterRepository> _logger)
        {
            _master = master;
            _logger = _logger;
        }

        [HttpGet("GetAllVariant/{type}")]
        public async Task<List<VariantDto>> GetAllVariant(string type)
        {

            return await _master.GetAllVariant(type);

        }

        [HttpGet("GetCategory")]
        public async Task<List<CategoryDto>> GetCategory()
        {

            return await _master.GetCategory();

        }
    }
}






