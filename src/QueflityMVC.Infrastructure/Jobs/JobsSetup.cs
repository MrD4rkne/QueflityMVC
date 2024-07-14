using Microsoft.Extensions.DependencyInjection;
using Quartz;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure.Jobs;

internal static class JobsSetup
{
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services, string connectionString)
    {
        services.AddTransient<IBackgroundJobScheduler, BackgroundJobScheduler>();
        services.AddQuartz(q =>
        {
            q.UseInMemoryStore();
            q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = 10; });

            q.AddJob<SendEmailJob>(opts => opts.WithIdentity(SendEmailJob.Key).StoreDurably());
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
}