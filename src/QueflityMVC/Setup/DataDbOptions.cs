using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;
using QueflityMVC.Web.Setup.Database;

namespace QueflityMVC.Web.Setup.Mails;

public class DatabaseOptions
{
    public const string SECTION_NAME = "Database";

    [Required] public required string ConnectionString { get; init; }

    public bool ShouldRetry { get; init; } = true;
    
    public PersistenceOptions AsPersistenceOptions()
    {
        return new PersistenceOptions
        {
            ConnectionString = ConnectionString,
            ShouldRetry = ShouldRetry
        };
    }
}

[OptionsValidator]
public partial class DatabaseOptionsValidator : IValidateOptions<DatabaseOptions>
{
}