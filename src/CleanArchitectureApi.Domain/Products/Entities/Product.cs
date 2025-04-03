using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Domain.Products.Entities;

public sealed class Product : BaseEntity
{
    private Product()
    {
        //
    }

    private Product(Guid id, Title description, Balance unitPrice) : base(id)
    {
        Description = description;
        unitPrice = unitPrice;
    }

    public Title Description { get; private set; }

    public Balance UnitPrice { get; private set; }

    public static Product Create(Title description, Balance unitPrice)
    {
        return new Product(
            Guid.NewGuid(),
            description,
            unitPrice
        );
    }

    public void Update(Title description, Balance unitPrice)
    {
        Description = description;
        UnitPrice = unitPrice;
    }
}