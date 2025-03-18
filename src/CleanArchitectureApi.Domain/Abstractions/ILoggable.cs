namespace CleanArchitectureApi.Domain.Abstractions;

public interface ILoggable
{
    public bool IsNotSuccessful { get; set; }

    public Error? Errors { get; set; }
}