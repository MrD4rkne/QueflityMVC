using Microsoft.Extensions.Options;
using QueflityMVC.Infrastructure.Emails;

namespace QueflityMVC.Web.Setup;

public class ConfigureEmails(IOptions<EmailsOptions> emailOptions) : IConfigureOptions<QuestionAskedConfig>,
    IConfigureOptions<EmailConfirmationConfig>, IConfigureOptions<PasswordResetEmailConfig>
{
    public void Configure(EmailConfirmationConfig options)
    {
        options.Subject = emailOptions.Value.EmailConfirmationOptions.Subject;
        options.Body = emailOptions.Value.EmailConfirmationOptions.Body;
    }

    public void Configure(PasswordResetEmailConfig options)
    {
        options.Subject = emailOptions.Value.PasswordResetOptions.Subject;
        options.Body = emailOptions.Value.PasswordResetOptions.Body;
    }

    public void Configure(QuestionAskedConfig options)
    {
        options.Subject = emailOptions.Value.QuestionAskedOptions.Subject;
        options.Body = emailOptions.Value.QuestionAskedOptions.Body;
    }
}