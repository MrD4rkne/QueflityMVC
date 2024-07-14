using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Repositories;
using QueflityMVC.Persistence.Seeding;

namespace QueflityMVC.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddTransient<IComponentRepository, ComponentRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IPurchasableRepository, PurchasableRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IKitRepository, KitRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }

    public static async Task SeedIdentity(this IServiceProvider serviceProvider, string[] claims = null)
    {
        using var scope = serviceProvider.CreateScope();
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await IdentitySeed.SeedIdentity(userManager, roleManager);
        if (claims is not null) await roleManager.SeedRolesClaims(claims);
    }
}