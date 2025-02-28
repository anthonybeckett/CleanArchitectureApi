using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using MediatR;

namespace CleanArchitectureApi.Application.Abstractions.Messaging.Commands;

public interface IBaseCommand;

public interface ICommand : IRequest<Result<NoContentDto>>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand where TResponse : IResult;