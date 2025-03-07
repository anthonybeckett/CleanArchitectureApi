namespace CleanArchitectureApi.Application.Abstractions.Emails;

public interface IEmailService
{
    Task SendAsync();
}