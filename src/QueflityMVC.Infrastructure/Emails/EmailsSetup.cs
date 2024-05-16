using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure.Emails;

internal static class EmailsSetup
{
    internal static IServiceCollection AddEmails(this IServiceCollection services, Action<SmtpConfig> configureOptions)
    {
        SmtpConfig smtpConfig = new();
        configureOptions(smtpConfig);
        services.AddSingleton<SmtpConfig>(smtpConfig);
        
        services.AddTransient<IEmailDispatcher, EmailDispatcher>();
        return services;
    }
}