using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class AdminKeyNotMatchException(List<string> errors) : Exception, IBadRequest
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "AdminKey.Error",
        ErrorMessage = errors
    };
}