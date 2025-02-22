using CleanArchitectureApi.Domain.Customers.ValueObjects;
using CleanArchitectureApi.Domain.Shared.ValueObjects;

namespace CleanArchitectureApi.Application.Customers.DTO;

public abstract class BaseCustomerDto
{
    public string? Title { get; set; }
    
    public string? AddressLine1 { get; set; }
    
    public string? AddressLine2 { get; set; }
    
    public string? Town { get; set; }
    
    public string? County { get; set; }
    
    public string? Postcode { get; set; }
    
    public string? Country { get; set; }
}