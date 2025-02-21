using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Customers.Events;

public record CustomerCreatedDomainEvent(Guid CustomerId) : IDomainEvent;