using AutoMapper;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;

namespace CleanArchitectureApi.Application.InvoiceItems.DTO;

public class InvoiceItemResponse
{
    public string? Description { get; init; }

    public decimal UnitPrice { get; init; }

    public int Quantity { get; init; }

    public decimal TotalPrice { get; init; }
}

public class InvoiceItemMapper : Profile
{
    public InvoiceItemMapper()
    {
        CreateMap<InvoiceItem, InvoiceItemResponse>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.SellPrice.Value))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Value))
            .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice.Value));
    }
}