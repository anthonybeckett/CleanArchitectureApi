using AutoMapper;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Application.InvoiceItems.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.InvoiceItems.Entities;
using CleanArchitectureApi.Domain.Invoices.Entities;

namespace CleanArchitectureApi.Application.Invoices.DTO;

public class InvoiceResponse : IResult
{
    public Guid Id { get; init; }

    public string? PoNumber { get; init; }

    public CustomerResponse? Customer { get; init; }

    public IReadOnlyCollection<InvoiceItemResponse>? PurchaseProducts { get; init; }

    public decimal InvoiceBalance { get; init; }
}

public class InvoiceResponseCollection : IResult
{
    public IReadOnlyCollection<InvoiceResponse>? Invoices { get; init; }
}

public class InvoiceMapper : Profile
{
    public InvoiceMapper()
    {
        CreateMap<Invoice, InvoiceResponse>()
            .ForMember(dest => dest.PoNumber, opt => opt.MapFrom(src => src.PoNumber.Value))
            .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer))
            .ForMember(dest => dest.InvoiceBalance, opt => opt.MapFrom(src => src.TotalBalance));
    }
}