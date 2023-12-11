namespace SalesOrderWebAPI.DTO.UserDto
{
    public class InvoiceEntity
    {
        public InvoiceHeader? header { get; set; }
        public List<InvoiceDetails>? details { get; set; }
    }
}
