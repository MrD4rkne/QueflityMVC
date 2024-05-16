using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Emails;
using QueflityMVC.Infrastructure.Jobs;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, Action<SmtpConfig> configureOptions, string connectionString)
    {
        ArgumentNullException.ThrowIfNull(configureOptions);
        
        services.AddTransient<IEmailDispatcher, EmailDispatcher>();
        services.AddBackgroundJobs(connectionString);
        services.AddEmails(configureOptions);
        return services;
    }
}