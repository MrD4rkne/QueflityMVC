namespace QueflityMVC.Infrastructure.Emails;

public class JobsConfig
{
    public bool UseDatabase { get; set; }

    public string? ConnectionString { get; set; }

    public bool WaitForJobsToComplete { get; set; } = true;

    public int MaxConcurrency { get; set; } = 10;
}