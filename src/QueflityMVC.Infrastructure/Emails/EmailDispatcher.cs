using System.Net;
using System.Net.Mail;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;

namespace QueflityMVC.Infrastructure.Purchasables;

public class EmailDispatcher : IEmailDispatcher
{
    private readonly SmtpClient _smtpClient;
    private readonly SmtpConfig _smtpConfig;

    public EmailDispatcher(SmtpConfig config)
    {
        // TODO: INJECT CONFIG
        _smtpConfig = config;
        _smtpClient = new SmtpClient();
        ConfigureSmtpClient(_smtpConfig);
    }

    public Task SendEmailAsync(Mail mail)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_smtpConfig.Username);
        mailMessage.To.Add(mail.Recipient);
        mailMessage.Subject = mail.Subject;
        mailMessage.Body = mail.Body;
        mailMessage.IsBodyHtml = true;
        return _smtpClient.SendMailAsync(mailMessage);
    }

    private void ConfigureSmtpClient(SmtpConfig smtpConfig)
    {
        _smtpClient.Host = smtpConfig.Host;
        _smtpClient.Port = smtpConfig.Port;
        _smtpClient.UseDefaultCredentials = false;
        _smtpClient.Credentials = new NetworkCredential(smtpConfig.Username, smtpConfig.Password);
        _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
        _smtpClient.EnableSsl = true;
    }
}