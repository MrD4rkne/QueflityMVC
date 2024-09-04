using QueflityMVC.Application.Results;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Emails;

public interface IEmailService
{
    Task<Result> SendEmailConfirmationAsync(EmailConfirmation emailConfirmation);

    Task<Result> SendResetPasswordEmail(ResetPasswordEmail resetPasswordEmail);
}