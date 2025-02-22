namespace CleanArchitectureApi.Application.Products.DTO;

public abstract class BaseProductDto
{
    public string? Description { get; set; }

    public decimal UnitPrice { get; set; }
}