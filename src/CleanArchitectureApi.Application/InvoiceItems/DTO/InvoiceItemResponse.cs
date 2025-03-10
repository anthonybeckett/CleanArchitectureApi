using AutoMapper;
using CleanArchitectureApi.Application.Invoices.DTO;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.Invoices.Entities;

namespace CleanArchitectureApi.Application.InvoiceItems.DTO;

public class InvoiceItemResponse
{
    public string? Description { get; init; }

    public int Quantity { get; init; }

    public decimal Price { get; init; }
}

public class InvoiceItemMapper : Profile
{
    public InvoiceItemMapper()
    {
        CreateMap<InvoiceItem, InvoiceItemResponse>()
            .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity.Value))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.TotalPrice));
    }
}