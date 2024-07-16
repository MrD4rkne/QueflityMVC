using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

internal class JobsOptions
{
    public const string SECTION_NAME = "Jobs";

    [Required] public bool UseDatabase { get; set; }

    public string? ConnectionString { get; set; }

    public bool WaitForJobsToComplete { get; set; } = true;

    [Range(1, int.MaxValue)] public int MaxConcurrency { get; set; } = 10;
}

[OptionsValidator]
internal partial class JobsOptionsValidator : IValidateOptions<JobsOptions>;