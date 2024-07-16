using Microsoft.AspNetCore.Builder;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using QueflityMVC.Persistence;

namespace QueflityMVC.Web.Setup.Database;

internal static class DbContextSetup
{
    internal static WebApplicationBuilder ConfigureDbContext<TContext>(this WebApplicationBuilder webApplicationBuilder) where TContext : DbContext
    {
        webApplicationBuilder.ConfigureDbConnection();
        return webApplicationBuilder;
    }

    private static WebApplicationBuilder ConfigureDbConnection(this WebApplicationBuilder appBuilder)
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