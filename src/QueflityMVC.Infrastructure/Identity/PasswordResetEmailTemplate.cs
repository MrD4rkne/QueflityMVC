using System.Text;
using Microsoft.Extensions.Options;
using QueflityMVC.Application.Emails;

namespace QueflityMVC.Infrastructure.Emails;

public class PasswordResetEmailTemplate(IOptions<PasswordResetEmailConfig> mailConfig)
    : EmailTemplate<ResetPasswordEmail>
{
    private const string USER_NAME_PLACEHOLDER = "{userName}";
    private const string EMAIL_PLACEHOLDER = "{email}";
    private const string RESET_LINK_PLACEHOLDER = "{resetLink}";

    public override Task<string> BuildSubject(ResetPasswordEmail model)
    {
        string subject = ApplyPlaceHolder(mailConfig.Value.Subject, model);
        return Task.FromResult(subject);
    }

    public override Task<string> BuildBody(ResetPasswordEmail model)
    {
        string body = ApplyPlaceHolder(mailConfig.Value.Body, model);
        return Task.FromResult(body);
    }

    private static string ApplyPlaceHolder(string template, ResetPasswordEmail model)
    {
        StringBuilder sb = new(template);
        sb.Replace(EMAIL_PLACEHOLDER, model.Email);
        sb.Replace(USER_NAME_PLACEHOLDER, model.User.UserName);
        sb.Replace(RESET_LINK_PLACEHOLDER, model.Url);
        return sb.ToString();
    }
}