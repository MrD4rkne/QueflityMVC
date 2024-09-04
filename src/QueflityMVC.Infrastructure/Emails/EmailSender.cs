using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using QueflityMVC.Application.Emails;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Emails;

public class EmailSender(
    IOptions<SmtpConfig> smtpOptions,
    EmailConfirmationTemplate emailConfirmationTemplate,
    PasswordResetEmailTemplate passwordResetEmailTemplate) : IEmailSender
{
    private readonly SmtpClient _smtpClient = new()
    {
        CheckCertificateRevocation = false
    };

    private readonly SmtpConfig config = smtpOptions.Value;

    public async Task SendMailAsync(Email email)
    {
        await _smtpClient.ConnectAsync(config.Host, config.Port, SecureSocketOptions.StartTls);
        await _smtpClient.AuthenticateAsync(config.Username, config.Password);

        await _smtpClient.SendAsync(CreateEmailMessage(email));

        await _smtpClient.DisconnectAsync(true);
    }

    public async Task SendEmailConfirmationAsync(EmailConfirmation emailConfirmation)
    {
        var mail = await emailConfirmationTemplate.BuildEmail(emailConfirmation, emailConfirmation.Email,
            emailConfirmation.User.UserName);
        await SendMailAsync(mail);
    }

    public async Task SendResetPasswordEmail(ResetPasswordEmail resetPasswordEmail)
    {
        var mail = await passwordResetEmailTemplate.BuildEmail(resetPasswordEmail, resetPasswordEmail.Email,
            resetPasswordEmail.User.UserName);
        await SendMailAsync(mail);
    }

    private MimeMessage CreateEmailMessage(Email email)
    {
        var mailMessage = new MimeMessage();
        mailMessage.From.Add(new MailboxAddress(config.Name, config.Email));
        mailMessage.To.Add(new MailboxAddress(email.RecipientName, email.RecipientEmail));
        mailMessage.Subject = email.Subject;
        mailMessage.Body = new TextPart("html")
        {
            Text = email.Body
        };
        return mailMessage;
    }
}