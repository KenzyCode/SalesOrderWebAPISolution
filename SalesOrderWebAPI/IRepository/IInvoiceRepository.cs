using SalesOrderWebAPI.DTO.UserDto;
using SalesOrderWebAPI.DTO.UserResponseDto;

namespace SalesOrderWebAPI.IRepository
{
    public interface IInvoiceRepository
    {

        Task<List<InvoiceHeader>> GetAllInvoiceHeader();
        Task<InvoiceHeader> GetAllInvoiceHeaderbyCode(string invoiceno);
        Task<List<InvoiceDetails>> GetAllInvoiceDetailbyCode(string invoiceno);
        Task<ResponseType> Save(InvoiceInput invoiceEntity);
        Task<ResponseType> Remove(string invoiceno);
    }
}
