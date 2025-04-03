using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class UserAlreadyExistsException(List<string> errors) : Exception
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "DuplicateUser.Error",
        ErrorMessage = errors
    };
}