using Microsoft.Build.Framework;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

public class EmailsOptions
{
    public const string SECTION_NAME = "Emails";

    [Required] [ValidateObjectMembers] public QuestionAskedEmail QuestionAskedOptions { get; set; }

    [Required] [ValidateObjectMembers] public EmailConfirmationEmail EmailConfirmationOptions { get; set; }

    [Required] [ValidateObjectMembers] public PasswordResetEmail PasswordResetOptions { get; set; }

    public class QuestionAskedEmail
    {
        [Required] public string Subject { get; set; }

        [Required] public string Body { get; set; }
    }

    public class EmailConfirmationEmail
    {
        [Required] public string Subject { get; set; }

        [Required] public string Body { get; set; }
    }

    public class PasswordResetEmail
    {
        [Required] public string Subject { get; set; }

        [Required] public string Body { get; set; }
    }
}

internal class EmailsOptionsValidator : IValidateOptions<EmailsOptions>
{
    public ValidateOptionsResult Validate(string? name, EmailsOptions options)
    {
        if (options == null)
        {
            return ValidateOptionsResult.Fail("EmailsOptions must be provided.");
        }

        if (options.QuestionAskedOptions == null)
        {
            return ValidateOptionsResult.Fail("QuestionAskedOptions must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.QuestionAskedOptions.Subject))
        {
            return ValidateOptionsResult.Fail("QuestionAskedOptions.Subject must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.QuestionAskedOptions.Body))
        {
            return ValidateOptionsResult.Fail("QuestionAskedOptions.Body must be provided.");
        }

        if (options.EmailConfirmationOptions == null)
        {
            return ValidateOptionsResult.Fail("EmailConfirmationOptions must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.EmailConfirmationOptions.Subject))
        {
            return ValidateOptionsResult.Fail("EmailConfirmationOptions.Subject must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.EmailConfirmationOptions.Body))
        {
            return ValidateOptionsResult.Fail("EmailConfirmationOptions.Body must be provided.");
        }

        if (options.PasswordResetOptions == null)
        {
            return ValidateOptionsResult.Fail("PasswordResetOptions must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.PasswordResetOptions.Subject))
        {
            return ValidateOptionsResult.Fail("PasswordResetOptions.Subject must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.PasswordResetOptions.Body))
        {
            return ValidateOptionsResult.Fail("PasswordResetOptions.Body must be provided.");
        }

        return ValidateOptionsResult.Success;
    }
}