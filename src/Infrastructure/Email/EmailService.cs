using System.Net.Mail;
using System.Net;
using System.Runtime;
using Microsoft.Extensions.Options;
using Contracts.Services.Email;

namespace Infrastructure.Email;

public class EmailService : IEmailService
{
    private readonly EmailSettings _emailSettings;

    public EmailService(IOptions<EmailSettings> settingsOptions)
    {
        _emailSettings = settingsOptions.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mensagem = new MailMessage
        {
            From = new MailAddress(_emailSettings.From, _emailSettings.DisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mensagem.To.Add(to);

        using var smtp = new SmtpClient(_emailSettings.Host, _emailSettings.Port)
        {
            Credentials = new NetworkCredential(_emailSettings.UserName, _emailSettings.Password),
            EnableSsl = _emailSettings.EnableSsl
        };

        await smtp.SendMailAsync(mensagem);
    }
}