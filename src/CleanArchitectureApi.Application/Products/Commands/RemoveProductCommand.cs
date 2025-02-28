using CleanArchitectureApi.Application.Abstractions.Messaging.Commands;

namespace CleanArchitectureApi.Application.Products.Commands;

public record RemoveProductCommand(Guid ProductId) : ICommand;