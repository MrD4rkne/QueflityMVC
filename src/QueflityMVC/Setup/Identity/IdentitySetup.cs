using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Setup.Identity;

public static class IdentitySetup
{
    public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddDefaultUI()
            .AddRoles<IdentityRole>()
            .AddSignInManager<MySignInManager>()
            .AddEntityFrameworkStores<Context>();
        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = true;
            options.Password.RequiredUniqueChars = 0;
            options.SignIn.RequireConfirmedEmail = false;
            options.User.RequireUniqueEmail = false;
        });

        services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.Zero; });

        return services;
    }
}