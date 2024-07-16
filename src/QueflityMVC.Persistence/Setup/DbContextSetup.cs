using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QueflityMVC.Persistence.Setup;

internal static class DbContextSetup
{
    internal static WebApplicationBuilder ConfigureDbConnection(this WebApplicationBuilder appBuilder)
    {
        try
        {
            appBuilder.Services.AddDbContext<Context>();

            if (appBuilder.Environment.IsDevelopment())
                appBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();

            return appBuilder;
        }
        catch (Exception ex)
        {
            throw new DbSetupException(
                "Exception occured while configuring database connection. See inner exception for more details.", ex);
        }
    }
}