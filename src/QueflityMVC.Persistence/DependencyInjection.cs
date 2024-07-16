using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Repositories;
using QueflityMVC.Persistence.Seeding;
using QueflityMVC.Persistence.Setup;

namespace QueflityMVC.Persistence;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder webApplicationBuilder)
    {
        return webApplicationBuilder.AddPersistence(_ => { });
    }

    public static WebApplicationBuilder AddPersistence(this WebApplicationBuilder webApplicationBuilder,
        Action<PersistenceConfig> configureOptions)
    {
        var services = webApplicationBuilder.Services;

        services.AddOptions<PersistenceConfig>().Configure(configureOptions);

        services.AddTransient<IComponentRepository, ComponentRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IPurchasableRepository, PurchasableRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IKitRepository, KitRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        webApplicationBuilder.ConfigureDbConnection();
        return webApplicationBuilder;
    }

    public static async Task SeedIdentity(this IServiceProvider serviceProvider, string[] claims = null)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentitySeed.SeedIdentity(userManager, roleManager);
        if (claims is not null) await roleManager.SeedRolesClaims(claims);
    }

    public static void ApplyPendingMigrations(this WebApplication webApplication)
    {
        try
        {
            webApplication.ApplyPendingMigrations<Context>();
        }
        catch (Exception ex)
        {
            webApplication.Logger.LogError(ex,
                "Exception occured while applying pending migrations. See inner exception for more details.");
            throw;
        }
    }

    private static void ApplyPendingMigrations<TContext>(this WebApplication webApplication) where TContext : DbContext
    {
        using var scope = webApplication.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        context.Database.EnsureCreated();
    }
}