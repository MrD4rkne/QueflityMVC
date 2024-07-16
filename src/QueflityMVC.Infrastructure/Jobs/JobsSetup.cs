using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Emails;
using QueflityMVC.Infrastructure.Purchasables;

namespace QueflityMVC.Infrastructure.Jobs;

internal static class JobsSetup
{
    internal static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddTransient<IBackgroundJobScheduler, BackgroundJobScheduler>();
        services.Configure<QuartzOptions>((options =>
        {
            options.AddJob<SendEmailJob>(opts => opts.WithIdentity(SendEmailJob.Key).StoreDurably());
        }));
        
        services.AddQuartz(q =>
        {
            var jobsOptions = services.BuildServiceProvider()
                .GetRequiredService<IOptions<JobsConfig>>().Value;
            if (jobsOptions.UseDatabase)
            {
                q.UsePersistentStore(storageOptions =>
                {
                    storageOptions.UseProperties = true;
                    storageOptions.UseSqlServer(jobsOptions.ConnectionString);
                    storageOptions.UseNewtonsoftJsonSerializer();
                });
            }
            else
            {
                q.UseInMemoryStore();
            }

            q.UseDefaultThreadPool(tp => { tp.MaxConcurrency = jobsOptions.MaxConcurrency; });
        });

        services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
        return services;
    }
}