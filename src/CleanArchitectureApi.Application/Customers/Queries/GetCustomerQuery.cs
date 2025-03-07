using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Customers.DTO;
using CleanArchitectureApi.Domain.Customers.Entities;

namespace CleanArchitectureApi.Application.Customers.Queries;

public record GetCustomerQuery(Guid CustomerId) : IQuery<CustomerResponse>;