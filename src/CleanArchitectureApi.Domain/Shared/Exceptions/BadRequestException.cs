using CleanArchitectureApi.Domain.Abstractions;

namespace CleanArchitectureApi.Domain.Shared.Exceptions;

public class BadRequestException(List<string> errors) : Exception
{
    public Error Errors { get; set; } = new()
    {
        ErrorCode = "BadRequest.Error",
        ErrorMessage = errors
    };
}