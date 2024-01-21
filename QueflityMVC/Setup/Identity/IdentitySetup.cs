using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Setup.Identity
{
    public static class IdentitySetup
    {
        public static IServiceCollection ConfigureIdentity(this IServiceCollection services)
        {
            services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<QueflityMVC.Infrastructure.Context>()
    .AddSignInManager<MySignInManager>();
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;
                options.Password.RequiredUniqueChars = 0;
                options.SignIn.RequireConfirmedEmail = false;
                options.User.RequireUniqueEmail = false;
            });

            return services;
        }
    }
}