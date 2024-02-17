using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Repositories;

namespace QueflityMVC.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IIngredientRepository, IngredientRepository>();
        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();
        services.AddTransient<IKitRepository, KitRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        return services;
    }

    public static async Task SeedData(this IServiceProvider serviceProvider)
    {
        using IServiceScope scope = serviceProvider.CreateScope();
        UserManager<ApplicationUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        await ContextSeed.SeedData(userManager,roleManager);
    }
}