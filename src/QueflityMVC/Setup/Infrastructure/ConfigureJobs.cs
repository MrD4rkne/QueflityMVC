using Microsoft.Extensions.Options;
using QueflityMVC.Infrastructure.Emails;

namespace QueflityMVC.Web.Setup;

internal class ConfigureJobs(IOptions<JobsOptions> jobOptions) : IConfigureOptions<JobsConfig>
{
    public void Configure(JobsConfig config)
    {
        config.UseDatabase = jobOptions.Value.UseDatabase;
        config.ConnectionString = jobOptions.Value.ConnectionString;
        config.MaxConcurrency = jobOptions.Value.MaxConcurrency;
        config.WaitForJobsToComplete = jobOptions.Value.WaitForJobsToComplete;
    }
}