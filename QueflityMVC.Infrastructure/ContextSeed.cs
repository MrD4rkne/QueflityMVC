using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Constants;
using QueflityMVC.Domain.Models;
using Serilog;

namespace QueflityMVC.Infrastructure;
public class ContextSeed
{
    private const string ADMIN_EMAIL = "admin@queflity.mvc";
    private const string ADMIN_DEFAULT_PASSWORD = "Password1#";

    public static async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        await SeedRoles(roleManager);
        await SeedAdmin(userManager);
        await SeedRolesClaims(roleManager);
    }

    private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            IdentityRole role = new("Admin");
            await roleManager.CreateAsync(role);
        }
    }

    private static async Task SeedRolesClaims(RoleManager<IdentityRole> roleManager)
    {
        string[] claims = {Claims.ENTITIES_LIST, Claims.ENTITIES_EDIT, Claims.ENTITIES_CREATE, Claims.USERS_LIST, Claims.USER_CLAIMS_MANAGE, Claims.USER_CLAIMS_VIEW, Claims.USER_DISABLE, Claims.USER_ENABLE, Claims.USER_ROLES_LIST, Claims.USER_ROLES_MANAGE};
        var adminRole = await roleManager.FindByNameAsync("Admin") ?? throw new Exception("Admin role not found");
        foreach(string claim in claims)
        {
            await roleManager.AddClaimAsync(adminRole, new(claim, claim));
        }
    }

    private static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
    {
        ApplicationUser? adminUser = await userManager.FindByEmailAsync(ADMIN_EMAIL);
        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = ADMIN_EMAIL,
                Email = ADMIN_EMAIL,
                IsEnabled = true
            };
            IdentityResult result = await userManager.CreateAsync(adminUser, ADMIN_DEFAULT_PASSWORD);
            if(!result.Succeeded)
            {
                Log.Error("Error while seeding admin user. {0}", result.Errors.ToString());
                return;
            }
            await userManager.ConfirmEmailAsync(adminUser, await userManager.GenerateEmailConfirmationTokenAsync(adminUser));
        }

        if(!await userManager.IsInRoleAsync(adminUser, "Admin")){
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
}
