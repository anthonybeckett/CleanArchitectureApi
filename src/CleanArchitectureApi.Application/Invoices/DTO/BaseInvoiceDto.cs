using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Invoices.DTO;

public abstract class BaseInvoiceDto
{
    [Required]
    [MaxLength(45)]
    public required string? PoNumber { get; set; }
}