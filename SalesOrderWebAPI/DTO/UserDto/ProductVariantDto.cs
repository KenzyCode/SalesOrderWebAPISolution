namespace SalesOrderWebAPI.DTO.UserDto
{
    public class ProductVariantDto
    {
        public int Id { get; set; }
        public string ProductCode { get; set; } = null!;
        public string? ColorCode { get; set; }
        public string? SizeCode { get; set; }
        public string? Remarks { get; set; }
        public decimal? Price { get; set; }
        public bool? IsActive { get; set; }
    }
}
