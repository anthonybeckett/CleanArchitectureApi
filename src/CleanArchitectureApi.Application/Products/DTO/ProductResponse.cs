using AutoMapper;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;

namespace CleanArchitectureApi.Application.Products;

public class ProductResponse : IResult
{
    public Guid Id { get; init; }

    public string Description { get; init; } = null!;

    public decimal UnitPrice { get; init; }
}

public class ProductMapper : Profile
{
    public ProductMapper()
    {
        CreateMap<Product, ProductResponse>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice.Value));
    }
}

public class ProductResponseCollection : IResult
{
    public IReadOnlyCollection<ProductResponse>? Products { get; set; }
}