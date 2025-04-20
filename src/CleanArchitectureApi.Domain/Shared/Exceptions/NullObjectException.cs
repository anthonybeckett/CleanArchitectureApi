using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class NullObjectException(List<string> errors) : Exception, IBadRequest
{
    public Error Error { get; set; } = new()
    {
        ErrorCode = "NullObject.Error",
        ErrorMessage = errors
    };
}