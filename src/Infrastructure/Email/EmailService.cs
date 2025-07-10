using System.Net;
using System.Net.Mail;
using Contracts.Services.Email;

namespace Infrastructure.Email;

public class EmailService(EmailSettings emailSettings) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var mensagem = new MailMessage
        {
            From = new MailAddress(emailSettings.From, emailSettings.DisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        mensagem.To.Add(to);

        using var smtp = new SmtpClient(emailSettings.Host, emailSettings.Port)
        {
            Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password),
            EnableSsl = emailSettings.EnableSsl
        };

        await smtp.SendMailAsync(mensagem);
    }
}