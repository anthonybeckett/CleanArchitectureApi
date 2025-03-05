using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Customers.DTO;

namespace CleanArchitectureApi.Application.Customers.Commands;

public record CreateCustomerCommand(CreateCustomerDto Dto) : ICommand<CustomerResponse>;