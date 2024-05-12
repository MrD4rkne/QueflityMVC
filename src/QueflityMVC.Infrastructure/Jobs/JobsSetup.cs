using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.AspNetCore;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure.Jobs;

internal static class JobsSetup
{
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IBackgroundJobScheduler, BackgroundJobScheduler>();
        services.AddQuartz();

        services.AddQuartzServer(options => { options.WaitForJobsToComplete = true; });
        return services;
    }
}