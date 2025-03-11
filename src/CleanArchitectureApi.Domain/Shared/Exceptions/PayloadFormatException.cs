using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class PayloadFormatException(List<string> errors) : Exception
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "PayloadFormat.Error",
        ErrorMessage = errors
    };
}