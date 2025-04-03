using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Customers.DTO;

public abstract class BaseCustomerDto
{
    [Required] [MaxLength(45)] public string? Title { get; set; }

    [Required] [MaxLength(40)] public string? AddressLine1 { get; set; }

    [MaxLength(40)] public string? AddressLine2 { get; set; }

    [Required] [MaxLength(40)] public string? Town { get; set; }

    [Required] [MaxLength(40)] public string? County { get; set; }

    [Required] [MaxLength(10)] public string? Postcode { get; set; }

    [Required] [MaxLength(40)] public string? Country { get; set; }
}