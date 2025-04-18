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
            RequestValidationException validationException => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                validationException.Error),

            ConcurrencyException concurrencyException => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                concurrencyException.Error),

            NullObjectException nullObjectException => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                nullObjectException.Error),

            BadRequestException badRequestException => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                badRequestException.Errors),

            PayloadFormatException payloadFormatException => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                payloadFormatException.Error),

            InternalServerException internalServerException => Result<NoContentDto>.Failure(
                HttpStatusCode.InternalServerError, internalServerException.Errors),

            InvalidTokenException invalidTokenException
                => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                    invalidTokenException.Errors),

            AdminKeyNotMatchException adminKeyNotMatchException
                => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                    adminKeyNotMatchException.Errors),

            UserAlreadyExistsException userAlreadyExistsException
                => Result<NoContentDto>.Failure(HttpStatusCode.BadRequest,
                    userAlreadyExistsException.Error),

            _ => Result<NoContentDto>.Failure(HttpStatusCode.InternalServerError, new Error
            {
                ErrorCode = "Internal Server Error",
                ErrorMessage = ["Unknown exception"]
            })
        };
    }
}