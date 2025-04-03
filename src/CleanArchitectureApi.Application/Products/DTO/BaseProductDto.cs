using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureApi.Application.Products.DTO;

public abstract class BaseProductDto
{
    [Required] [MaxLength(45)] public string? Description { get; set; }

    [Required] public decimal UnitPrice { get; set; }
}