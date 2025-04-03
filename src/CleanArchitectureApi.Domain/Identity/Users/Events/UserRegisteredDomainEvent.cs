using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Identity.Users.Events;

public record UserRegisteredDomainEvent(
    Guid UserId,
    string? AdminKey
) : IDomainEvent;