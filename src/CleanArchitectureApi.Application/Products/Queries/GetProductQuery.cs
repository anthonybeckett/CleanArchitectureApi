using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;

namespace CleanArchitectureApi.Application.Products.Queries;

public record GetProductQuery(Guid ProductId) : IQuery<ProductResponse>, ICachedQuery
{
    public string CacheKey => $"product-{ProductId}";

    public TimeSpan? Expiration => null;
}