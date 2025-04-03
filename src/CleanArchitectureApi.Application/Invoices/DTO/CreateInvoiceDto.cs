using System.ComponentModel.DataAnnotations;
using CleanArchitectureApi.Application.InvoiceItems.DTO;

namespace CleanArchitectureApi.Application.Invoices.DTO;

public class CreateInvoiceDto : BaseInvoiceDto
{
    [Required] public Guid CustomerId { get; set; }

    [Required] public ICollection<CreateInvoiceItemDto> PurchasedProducts { get; set; }
}