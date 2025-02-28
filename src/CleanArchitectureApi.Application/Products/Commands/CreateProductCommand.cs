using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;
using CleanArchitectureApi.Application.Products.DTO;

namespace CleanArchitectureApi.Application.Products.Commands;

public record CreateProductCommand(
    CreateProductDto Dto
) : ICommand<ProductResponse>;