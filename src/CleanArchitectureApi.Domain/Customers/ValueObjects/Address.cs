namespace CleanArchitectureApi.Domain.Customers.ValueObjects;

public record Address(
    string AddressLine1,
    string? AddressLine2,
    string Town,
    string County,
    string Postcode,
    string Country);