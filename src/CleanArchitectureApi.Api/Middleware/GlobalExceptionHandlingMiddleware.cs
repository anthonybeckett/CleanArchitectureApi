using System.Net;
using CleanArchitectureApi.Application.Abstractions.DTO;
using CleanArchitectureApi.Domain.Abstractions;
using CleanArchitectureApi.Domain.Shared.Exceptions;
using Serilog.Context;

namespace CleanArchitectureApi.Api.Middleware;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            var exceptionDetails = GetExceptionDetails(exception);

            using (LogContext.PushProperty("Error", exceptionDetails.Errors!.ErrorMessage, true))
            {
                logger.LogError(exception, exception.Message);
            }

            httpContext.Response.StatusCode = (int)exceptionDetails.StatusCode;

            await httpContext.Response.WriteAsJsonAsync(exceptionDetails);
        }
    }

    private static Result<NoContentDto> GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            IBadRequest badRequestException
                => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                    badRequestException.Error
                ),

            IInternalServerError internalServerException
                => Result<NoContentDto>.Failure(
                    HttpStatusCode.InternalServerError, internalServerException.Error
                ),

            _ => Result<NoContentDto>.Failure(HttpStatusCode.InternalServerError, new Error
            {
                ErrorCode = "Internal Server Error",
                ErrorMessage = ["Unknown exception"]
            })
        };
    }
}