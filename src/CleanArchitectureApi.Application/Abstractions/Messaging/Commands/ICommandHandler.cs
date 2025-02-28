using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using MediatR;

namespace CleanArchitectureApi.Application.Abstractions.Messaging.Commands;

public interface ICommandHandler<in TCommand> : IRequestHandler<TCommand, Result<NoContentDto>>
    where TCommand : ICommand;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>
    where TResponse : IResult;