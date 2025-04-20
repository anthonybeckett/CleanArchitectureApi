namespace CleanArchitectureApi.Domain.Abstractions;

public interface IInternalServerError
{
    public Error Error { get; set; }
}