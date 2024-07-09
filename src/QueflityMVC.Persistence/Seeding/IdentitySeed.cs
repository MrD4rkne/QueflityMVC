using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;
using Serilog;

namespace QueflityMVC.Persistence.Seeding;

public static class IdentitySeed
{
    private const string ADMIN_EMAIL = "admin@queflity.mvc";
    private const string ADMIN_DEFAULT_PASSWORD = "Password1#";

    public static async Task SeedIdentity(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        ArgumentNullException.ThrowIfNull(userManager, nameof(userManager));
        ArgumentNullException.ThrowIfNull(roleManager, nameof(roleManager));
        await SeedRoles(roleManager);
        await SeedAdmin(userManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            IdentityRole role = new("Admin");
            await roleManager.CreateAsync(role);
        }
    }

    public static async Task SeedRolesClaims(this RoleManager<IdentityRole> roleManager, string[] claims)
    {
        var adminRole = await roleManager.FindByNameAsync("Admin") ?? throw new Exception("Admin role not found");
        foreach (var claim in claims) await roleManager.AddClaimAsync(adminRole, new Claim(claim, claim));
    }

    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        var adminUser = await userManager.FindByEmailAsync(ADMIN_EMAIL);
        if (adminUser is null) 
        {
            adminUser = new ApplicationUser
            {
                UserName = ADMIN_EMAIL,
                Email = ADMIN_EMAIL,
                IsEnabled = true
            };
            var result = await userManager.CreateAsync(adminUser, ADMIN_DEFAULT_PASSWORD);
            if (!result.Succeeded)
            {
                Log.Error("Error while seeding admin user. {0}", result.Errors.ToString());
                return;
            }

            await userManager.ConfirmEmailAsync(adminUser,
                await userManager.GenerateEmailConfirmationTokenAsync(adminUser));
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
            try
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Error while seeding admin user");
                await userManager.DeleteAsync(adminUser);
            }
    }
}