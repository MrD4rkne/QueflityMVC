using Microsoft.AspNetCore.Identity.UI.Services;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

namespace QueflityMVC.Web.Common;

public class IdentityEmailSender(IEmailDispatcher emailDispatcher) : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Mail mail = new()
        {
            RecipientEmail = email,
            Subject = subject,
            Body = htmlMessage
        };

        return emailDispatcher.SendEmailAsync(mail);
    }
}