using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Emails;
using QueflityMVC.Infrastructure.Jobs;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        Action<SmtpConfig> configureSmtpOptions, Action<JobsConfig> configureJobsOptions)
    {
        ArgumentNullException.ThrowIfNull(configureSmtpOptions);

        services.AddOptions<SmtpConfig>().Configure(configureSmtpOptions);
        services.AddOptions<JobsConfig>().Configure(configureJobsOptions);

        services.AddTransient<IEmailDispatcher, EmailDispatcher>();
        services.AddBackgroundJobs();
        services.AddEmails();
        return services;
    }
}