using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class AdminKeyNotMatchException(List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "AdminKey.Error",
        ErrorMessage = errors
    };
}