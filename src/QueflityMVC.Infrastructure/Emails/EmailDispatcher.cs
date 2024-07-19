using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

namespace QueflityMVC.Infrastructure.Emails;

public class EmailDispatcher(IOptions<SmtpConfig> smtpOptions) : IEmailDispatcher
{
    private readonly SmtpClient _smtpClient = new()
    {
        CheckCertificateRevocation = false
    };

    private readonly SmtpConfig config = smtpOptions.Value;

    public async Task SendEmailAsync(Mail mail)
    {
        await _smtpClient.ConnectAsync(config.Host, config.Port, SecureSocketOptions.StartTls);
        await _smtpClient.AuthenticateAsync(config.Username, config.Password);

        await _smtpClient.SendAsync(CreateEmailMessage(mail));

        await _smtpClient.DisconnectAsync(true);
    }

    private MimeMessage CreateEmailMessage(Mail mail)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress("Queflity", config.Email));
        mailMessage.To.Add(new MailboxAddress("sss", mail.Recipient));
        mailMessage.Subject = mail.Subject;
        mailMessage.Body = new TextPart("plain")
        {
            Text = mail.Body
        };
        return mailMessage;
    }
}