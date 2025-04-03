using CleanArchitectureApi.Application.Abstractions.Cache;
using CleanArchitectureApi.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitectureApi.Application.Abstractions.Behaviours;

public class CachingBehaviour<TRequest, TResponse>(
    ICacheService cacheService,
    ILogger<CachingBehaviour<TRequest, TResponse>> logger
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
    where TResponse : ILoggable
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cachedResult = await cacheService.GetAsync<TResponse>(request.CacheKey, cancellationToken);

        var requestName = typeof(TRequest).Name;

        if (cachedResult != null)
        {
            logger.LogInformation($"{requestName} cached");

            return cachedResult;
        }

        logger.LogInformation($"{requestName} has no cache");

        var result = await next();

        if (!result.IsNotSuccessful)
            await cacheService.SetAsync(
                request.CacheKey,
                result,
                request.Expiration,
                cancellationToken
            );

        return result;
    }
}