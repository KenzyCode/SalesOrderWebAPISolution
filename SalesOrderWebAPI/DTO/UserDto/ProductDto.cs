using System.ComponentModel.DataAnnotations;

namespace SalesOrderWebAPI.DTO.UserDto
{
    public class ProductDto
    {
        public string Code { get; set; } = null!;
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public int? Category { get; set; }
        public string? productImage { get; set; }
        public string? Remarks { get; set; }
        public List<ProductVariantDto>? Variants { get; set; }
    }
}
