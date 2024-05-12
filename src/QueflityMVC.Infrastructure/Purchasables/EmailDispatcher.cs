using System.Net;
using System.Net.Mail;
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

    public Task SendEmailAsync(string recipient, string subject, string body)
    {
        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(_smtpConfig.Username);
        mailMessage.To.Add(recipient);
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = true;
        return new SmtpClient().SendMailAsync(mailMessage);
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