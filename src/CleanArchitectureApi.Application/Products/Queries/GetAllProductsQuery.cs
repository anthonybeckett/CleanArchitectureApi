using CleanArchitectureApi.Application.Abstractions.Messaging.Queries;

namespace CleanArchitectureApi.Application.Products.Queries;

public record GetAllProductsQuery : IQuery<ProductResponseCollection>;