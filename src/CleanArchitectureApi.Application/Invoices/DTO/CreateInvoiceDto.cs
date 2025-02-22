using CleanArchitectureApi.Application.InvoiceItems.DTO;

namespace CleanArchitectureApi.Application.Invoices.DTO;

public class CreateInvoiceDto : BaseInvoiceDto
{
    public Guid CustomerId { get; set; }

    public ICollection<CreateInvoiceItemDto> PurchasedProducts { get; set; }
}