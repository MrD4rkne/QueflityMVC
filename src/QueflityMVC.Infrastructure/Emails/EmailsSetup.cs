using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

namespace QueflityMVC.Infrastructure.Emails;

internal static class EmailsSetup
{
    internal static IServiceCollection AddEmails(this IServiceCollection services)
    {
        services.AddTransient<IEmailDispatcher, EmailDispatcher>();
        return services;
    }
}