using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Jobs;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IEmailDispatcher, EmailDispatcher>();
        services.AddBackgroundJobs(connectionString);
        return services;
    }
}