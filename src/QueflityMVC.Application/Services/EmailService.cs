using Microsoft.Extensions.Logging;
using QueflityMVC.Application.Emails;
using QueflityMVC.Application.Results;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class EmailService(IEmailSender emailSender, ILogger<EmailService> logger) : IEmailService
{
    public async Task<Result> SendEmailConfirmationAsync(EmailConfirmation emailConfirmation)
    {
        try
        {
            await emailSender.SendEmailConfirmationAsync(emailConfirmation);
            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not send email confirmation email");
            return Result.Failure(ErrorCodes.Emails.COULD_NOT_SEND, e.Message);
        }
    }

    public async Task<Result> SendResetPasswordEmail(ResetPasswordEmail resetPasswordEmail)
    {
        try
        {
            await emailSender.SendResetPasswordEmail(resetPasswordEmail);
            return Result.Success();
        }
        catch (Exception e)
        {
            logger.LogError(e, "Could not send reset password email");
            return Result.Failure(Errors.Emails.CouldNotSentEmail);
        }
    }
}