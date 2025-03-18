using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using MediatR;

namespace CleanArchitectureApi.Application.Abstractions.Messaging.Commands;

public interface ICommand : IRequest<Result<NoContentDto>>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>> where TResponse : IResult;