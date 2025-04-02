using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class InternalServerException(string errorCode, List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = errorCode,
        ErrorMessage = errors
    };
}