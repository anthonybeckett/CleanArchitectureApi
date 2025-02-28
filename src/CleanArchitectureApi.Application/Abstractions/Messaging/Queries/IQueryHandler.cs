using CleanArchitectureApi.Domain.Abstractions;
using MediatR;

namespace CleanArchitectureApi.Application.Abstractions.Messaging.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
    where TResponse : IResult;