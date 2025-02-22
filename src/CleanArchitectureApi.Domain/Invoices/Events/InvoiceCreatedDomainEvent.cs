using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Invoices.Events;

public record InvoiceCreatedDomainEvent(Guid InvoiceId) : IDomainEvent;