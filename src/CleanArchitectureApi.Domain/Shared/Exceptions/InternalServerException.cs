using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class InternalServerException(string errorCode, List<string> errors) : Exception, IInternalServerError
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = errorCode,
        ErrorMessage = errors
    };
}