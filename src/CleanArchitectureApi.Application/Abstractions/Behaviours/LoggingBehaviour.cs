using CleanArchitectureApi.Domain.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace CleanArchitectureApi.Application.Abstractions.Behaviours;

public class LoggingBehaviour<TRequest, TResponse>(ILogger<TRequest> logger) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseRequest
    where TResponse : ILoggable
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;

        try
        {
            logger.LogInformation($"Executing request: {requestName}");
            
            var result = await next();

            if (result.IsNotSuccessful)
            {
                using (LogContext.PushProperty("Error", result.Errors!.ErrorMessage, true))
                {
                    logger.LogError($"Request failed: {requestName}");
                }
            }
            
            logger.LogInformation($"Request successful: {requestName}");

            return result;
        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"Request failed: {requestName}");

            throw;
        }
    }
}