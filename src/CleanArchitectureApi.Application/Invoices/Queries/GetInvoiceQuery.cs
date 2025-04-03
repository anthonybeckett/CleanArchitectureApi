using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Invoices.DTO;

namespace CleanArchitectureApi.Application.Invoices.Queries;

public record GetInvoiceQuery(Guid InvoiceId) : IQuery<InvoiceResponse>, ICachedQuery
{
    public string CacheKey => $"invoice-{InvoiceId}";

    public TimeSpan? Expiration => null;
}