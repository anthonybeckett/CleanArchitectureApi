using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Customers.DTO;

namespace CleanArchitectureApi.Application.Customers.Queries;

public record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>, ICachedQuery
{
    public string CacheKey => $"customer-{CustomerId}";

    public TimeSpan? Expiration => null;
}