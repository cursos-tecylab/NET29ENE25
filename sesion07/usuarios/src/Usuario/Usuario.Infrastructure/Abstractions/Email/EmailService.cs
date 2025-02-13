
using Usuario.Application.Abstractions.Email;

namespace Usuario.Infrastructure.Abstractions.Email;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string email, string subject, string message)
    {
       return Task.CompletedTask;
    }
}