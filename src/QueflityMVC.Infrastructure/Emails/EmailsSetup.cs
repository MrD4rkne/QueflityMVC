using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Application.Emails;
using QueflityMVC.Application.Notifications;
using QueflityMVC.Infrastructure.Jobs;

namespace QueflityMVC.Infrastructure.Emails;

internal static class EmailsSetup
{
    internal static IServiceCollection AddEmails(this IServiceCollection services)
    {
        services.AddTransient<INotificationsService, EmailNotificationService>();
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddTransient<QuestionAskedEmailTemplate>();
        services.AddTransient<EmailConfirmationTemplate>();
        services.AddTransient<PasswordResetEmailTemplate>();
        return services;
    }
}