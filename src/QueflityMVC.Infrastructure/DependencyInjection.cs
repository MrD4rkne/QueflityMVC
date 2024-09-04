using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Emails;
using QueflityMVC.Infrastructure.Jobs;

namespace QueflityMVC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services.AddInfrastructure(_ => { },
            _ => { },
            _ => { },
            _ => { },
            _ => { }
        );
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<SmtpConfig> configureSmtpOptions,
        Action<JobsConfig> configureJobsOptions,
        Action<QuestionAskedConfig> configureQuestionAskedOptions,
        Action<EmailConfirmationConfig> configureEmailConfirmationOptions,
        Action<PasswordResetEmailConfig> configurePasswordResetEmailOptions
    )
    {
        ArgumentNullException.ThrowIfNull(configureSmtpOptions);

        services.AddOptions<SmtpConfig>().Configure(configureSmtpOptions);
        services.AddOptions<JobsConfig>().Configure(configureJobsOptions);
        services.AddOptions<QuestionAskedConfig>().Configure(configureQuestionAskedOptions);
        services.AddOptions<EmailConfirmationConfig>().Configure(configureEmailConfirmationOptions);
        services.AddOptions<PasswordResetEmailConfig>().Configure(configurePasswordResetEmailOptions);

        services.AddBackgroundJobs();
        services.AddEmails();

        return services;
    }
}