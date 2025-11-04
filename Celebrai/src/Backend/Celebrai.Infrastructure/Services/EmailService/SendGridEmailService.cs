using Celebrai.Domain.Services.EmailService;
using Celebrai.Exceptions.ExceptionsBase;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Celebrai.Infrastructure.Services.EmailService;
public class SendGridEmailService : IEmailService
{
    private readonly ISendGridClient _sendGridClient;
    private readonly string _fromEmail;
    private readonly string _fromName;

    public SendGridEmailService(ISendGridClient sendGridClient, string fromEmail, string fromName)
    {
        _sendGridClient = sendGridClient;
        _fromEmail = fromEmail;
        _fromName = fromName;
    }

    public async Task SendEmail(string toEmail, string subject, string bodyHtml)
    {
        var from = new EmailAddress(_fromEmail, _fromName);

        var to = new EmailAddress(toEmail);

        var plainTextContent = "Este e-mail requer um cliente que suporte HTML.";

        var msg = MailHelper.CreateSingleEmail(
            from,
            to,
            subject,
            plainTextContent,
            bodyHtml
        );

        var response = await _sendGridClient.SendEmailAsync(msg);

        if (response.IsSuccessStatusCode == false)
        {
            var body = await response.Body.ReadAsStringAsync();
            throw new EmailException($"Falha ao enviar e-mail. Status: {response.StatusCode}. Body: {body}");
        }
    }
}
