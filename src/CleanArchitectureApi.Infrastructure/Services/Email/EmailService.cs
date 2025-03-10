using CleanArchitectureApi.Application.Abstractions.Emails;

namespace CleanArchitectureApi.Infrastructure.Services.Email;

public class EmailService : IEmailService
{
    public Task SendAsync()
    {
        return Task.CompletedTask;
    }
}