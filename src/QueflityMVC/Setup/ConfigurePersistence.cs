using Microsoft.Extensions.Options;
using QueflityMVC.Persistence.Setup;

namespace QueflityMVC.Web.Setup;

internal class ConfigurePersistence(IOptions<DatabaseOptions> dbOptions) : IConfigureOptions<PersistenceConfig>
{
    public void Configure(PersistenceConfig options)
    {
        options.ConnectionString = dbOptions.Value.ConnectionString;
        options.ShouldRetry = dbOptions.Value.ShouldRetry;
    }
}