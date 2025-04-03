namespace CleanArchitectureApi.Domain.Abstractions;

public interface IDomainEventRaiser
{
    IReadOnlyList<IDomainEvent> GetDomainEvents();

    void ClearDomainEvents();

    void RaiseDomainEvent(IDomainEvent domainEvent);
}