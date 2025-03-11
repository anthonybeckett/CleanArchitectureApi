using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class RequestValidationException(List<string> errors) : Exception
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "Validation.Error",
        ErrorMessage = errors
    };
}