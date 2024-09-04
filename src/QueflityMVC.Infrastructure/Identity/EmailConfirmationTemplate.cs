using System.Text;
using Microsoft.Extensions.Options;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Emails;

public class EmailConfirmationTemplate(IOptions<EmailConfirmationConfig> mailConfig) : EmailTemplate<EmailConfirmation>
{
    private const string USER_NAME_PLACEHOLDER = "{userName}";
    private const string EMAIL_PLACEHOLDER = "{email}";
    private const string CONFIRMATION_LINK_PLACEHOLDER = "{confirmationLink}";


    public override Task<string> BuildSubject(EmailConfirmation model)
    {
        string subject = ApplyPlaceHolder(mailConfig.Value.Subject, model);
        return Task.FromResult(subject);
    }

    public override Task<string> BuildBody(EmailConfirmation model)
    {
        string body = ApplyPlaceHolder(mailConfig.Value.Body, model);
        return Task.FromResult(body);
    }

    private static string ApplyPlaceHolder(string template, EmailConfirmation model)
    {
        StringBuilder sb = new(template);
        sb.Replace(EMAIL_PLACEHOLDER, model.Email);
        sb.Replace(USER_NAME_PLACEHOLDER, model.User.UserName);
        sb.Replace(CONFIRMATION_LINK_PLACEHOLDER, model.Url);
        return sb.ToString();
    }
}