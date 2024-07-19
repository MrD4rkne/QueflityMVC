using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

internal class DatabaseOptions
{
    public const string SECTION_NAME = "Database";

    [Required] public required string ConnectionString { get; init; }

    public bool ShouldRetry { get; init; } = true;
}

[OptionsValidator]
internal partial class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>;