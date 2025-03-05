using AutoMapper;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Customers.Entities;
using CleanArchitectureApi.Domain.Customers.ValueObjects;

namespace CleanArchitectureApi.Application.Customers.DTO;

public class CustomerResponse : IResult
{
    public Guid Id { get; set; }

    public string? Title { get; set; }

    public Address? Address { get; set; }

    public decimal Balance { get; set; }
}

public class CustomerResponseCollection : IResult
{
    public List<CustomerResponse>? Customers { get; set; }
}

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, CustomerResponse>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title.Value))
            .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.Balance.Value));
    }
}