using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Customers.DTO;

namespace CleanArchitectureApi.Application.Customers.Commands;

public record UpdateCustomerCommand(Guid CustomerId, UpdateCustomerDto Dto) : ICommand;