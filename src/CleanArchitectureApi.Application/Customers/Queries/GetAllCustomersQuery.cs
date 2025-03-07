using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;
using CleanArchitectureApi.Application.Customers.DTO;

namespace CleanArchitectureApi.Application.Customers.Queries;

public record GetAllCustomersQuery() : IQuery<CustomerResponseCollection>;