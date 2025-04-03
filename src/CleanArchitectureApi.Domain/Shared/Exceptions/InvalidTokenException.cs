using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class InvalidTokenException(List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "InvalidToken.Error",
        ErrorMessage = errors
    };
}