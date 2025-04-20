namespace CleanArchitectureApi.Domain.Abstractions;

public interface IBadRequest
{
    public Error Error { get; set; }
}