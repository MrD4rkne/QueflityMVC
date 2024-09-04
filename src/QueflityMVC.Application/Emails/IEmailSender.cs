using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Emails;

public interface IEmailSender
{
    Task SendMailAsync(Email email);

    Task SendEmailConfirmationAsync(EmailConfirmation emailConfirmation);

    Task SendResetPasswordEmail(ResetPasswordEmail resetPasswordEmail);
}