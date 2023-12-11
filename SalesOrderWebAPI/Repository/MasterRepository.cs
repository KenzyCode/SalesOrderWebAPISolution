using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesOrderWebAPI.Data.DataContext;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.IRepository;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Repository
{

    public class MasterRepository : IMasterRepository
    {
        private readonly SalesOrderDbContext _dBContext;
        private readonly IMapper _mapper;

        public MasterRepository(SalesOrderDbContext DBContext, IMapper mapper)
        {
            _dBContext = DBContext;
            _mapper = mapper;
        }


        public async Task<List<VariantDto>> GetAllVariant(string variantType)
        {
            var customerdata = await _dBContext.TblMastervariants.Where(item => item.VarinatType == variantType).ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                // we need use automapper

                return _mapper.Map<List<TblMastervariant>, List<VariantDto>>(customerdata);
            }
            return new List<VariantDto>();

        }

        public async Task<List<CategoryDto>> GetCategory()
        {
            var customerdata = await _dBContext.TblCategories.ToListAsync();
            if (customerdata != null && customerdata.Count > 0)
            {
                // we need use automapper

                return _mapper.Map<List<TblCategory>, List<CategoryDto>>(customerdata);
            }
            return new List<CategoryDto>();

        }

    }
}

