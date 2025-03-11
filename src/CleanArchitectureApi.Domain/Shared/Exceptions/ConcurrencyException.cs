using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class ConcurrencyException(List<string> errors) : Exception
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "Concurrency.Error",
        ErrorMessage = errors
    };
}