using AutoMapper;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Products.Entities;

namespace CleanArchitectureApi.Application.Products;

public class ProductResponse : IResult
{
    public Guid Id { get; set; }

    public string Description { get; set; }

    public decimal UnitPrice { get; set; }
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