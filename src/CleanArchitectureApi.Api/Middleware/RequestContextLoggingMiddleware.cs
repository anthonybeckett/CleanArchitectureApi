using Serilog.Context;

namespace CleanArchitectureApi.Api.Middleware;

public class RequestContextLoggingMiddleware(RequestDelegate next)
{
    private const string CORRELATION_ID_HEADER_NAME = "x-correlation-id";

    public Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("CorrelationId", GetCorrelationId(context)))
        {
            return next(context);
        }
    }

    private static string GetCorrelationId(HttpContext context)
    {
        context.Request.Headers.TryGetValue(CORRELATION_ID_HEADER_NAME, out var value);

        return value.FirstOrDefault() ?? context.TraceIdentifier;
    }
}