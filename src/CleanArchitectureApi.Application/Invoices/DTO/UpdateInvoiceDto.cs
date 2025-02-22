namespace CleanArchitectureApi.Application.Invoices.DTO;

public class UpdateInvoiceDto : BaseInvoiceDto
{
    public Guid InvoiceId { get; set; }
}