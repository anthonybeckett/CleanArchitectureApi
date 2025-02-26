using CleanArchitectureApi.Domain.Abstractions;
using MediatR;

namespace CleanArchitectureApi.Application.Abstractions.Messaging.Queries;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;