using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;

namespace CleanArchitectureApi.Application.Customers.Commands;

public record RemoveCustomerCommand(Guid CustomerId) : ICommand;