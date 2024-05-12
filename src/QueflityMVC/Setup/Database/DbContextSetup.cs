using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Web.Common;
using QueflityMVC.Web.Setup.Secrets;

namespace QueflityMVC.Web.Setup.Database;

public static class DbContextSetup
{
    public static bool TryBuildConnectionString(string? connectionString, out string formatedConnectionString)
    {
        if (string.IsNullOrEmpty(connectionString))
        {
            formatedConnectionString = string.Empty;
            return false;
        }

        var connectionStringBuilder = new SqlConnectionStringBuilder();
        connectionStringBuilder.ConnectionString = connectionString;
        formatedConnectionString = connectionStringBuilder.ToString();
        return true;
    }

    public static IServiceCollection ConfigureDbContext<TContext>(this IServiceCollection services,
        IVariablesProvider variablesProvider) where TContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(variablesProvider);

        var builtConnectionString = PrepareConnectionString(variablesProvider);
        services.ConfigureConnection<TContext>(builtConnectionString);

        return services;
    }

    public static void ApplyPendingMigrations<TContext>(this WebApplication appBuilder) where TContext : DbContext
    {
        using var scope = appBuilder.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.EnsureCreated();
    }

    private static string PrepareConnectionString(IVariablesProvider variablesProvider)
    {
        ArgumentNullException.ThrowIfNull(variablesProvider);
        try
        {
            if (!TryBuildConnectionString(variablesProvider.GetConnectionString(), out var builtConnectionString))
                throw new ConfigurationException(
                    "Connection string is invalid. Please fix this and provide valid one.");
            return builtConnectionString;
        }
        catch (Exception ex)
        {
            throw new DbSetupException("Connection string exception occured. See inner exception for more details.",
                ex);
        }
    }

    private static IServiceCollection ConfigureConnection<TContext>(this IServiceCollection services,
        string connectionString) where TContext : DbContext
    {
        try
        {
            services.AddDbContext<TContext>(options =>
                options.UseSqlServer(connectionString,
                    provider => provider.EnableRetryOnFailure()
                ));

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
                services.AddDatabaseDeveloperPageExceptionFilter();

            return services;
        }
        catch (Exception ex)
        {
            throw new DbSetupException(
                "Exception occured while configuring database connection. See inner exception for more details.", ex);
        }
    }
}