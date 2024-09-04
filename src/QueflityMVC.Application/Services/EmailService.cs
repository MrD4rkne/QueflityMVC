using QueflityMVC.Application.Emails;
using QueflityMVC.Application.Results;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class EmailService(IEmailSender emailSender) : IEmailService
{
    public async Task<Result> SendEmailConfirmationAsync(EmailConfirmation emailConfirmation)
    {
        await emailSender.SendEmailConfirmationAsync(emailConfirmation);
        return Result.Success();
    }

    public async Task<Result> SendResetPasswordEmail(ResetPasswordEmail resetPasswordEmail)
    {
        await emailSender.SendResetPasswordEmail(resetPasswordEmail);
        return Result.Success();
    }
}