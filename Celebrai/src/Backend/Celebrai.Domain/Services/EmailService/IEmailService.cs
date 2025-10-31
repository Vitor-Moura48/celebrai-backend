namespace Celebrai.Domain.Services.EmailService;
public interface IEmailService
{
    public Task SendEmail(string toEmail, string subject, string bodyHtml);
}
