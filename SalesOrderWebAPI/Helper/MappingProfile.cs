
using AutoMapper;
using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.Model;

namespace SalesOrderWebAPI.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<TblCustomer, CustomerDto>().ForMember(item => item.StatusName, item => item.MapFrom(s => s.IsActive == true ? "Active" : "In Active"));
            CreateMap<TblSalesHeader, InvoiceHeader>().ReverseMap();
            CreateMap<TblSalesProductInfo, InvoiceDetails>().ReverseMap();
            CreateMap<TblProduct, ProductDto>().ReverseMap();
            CreateMap<TblProductvarinat, ProductVariantDto>().ReverseMap();
            CreateMap<TblMastervariant, VariantDto>().ReverseMap();
            CreateMap<TblCategory, CategoryDto>().ReverseMap();
            CreateMap<TblSalesHeader, InvoiceInput>().ReverseMap();
        }
    }
}
