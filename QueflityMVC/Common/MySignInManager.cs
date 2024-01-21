using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Web.Common
{
    public class MySignInManager : SignInManager<ApplicationUser>
    {
        public MySignInManager(UserManager<ApplicationUser> userManager, IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory, IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<ApplicationUser>> logger, IAuthenticationSchemeProvider schemes, IUserConfirmation<ApplicationUser> confirmation) : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override Task<SignInResult> PasswordSignInAsync(ApplicationUser user, string password, bool isPersistent, bool lockoutOnFailure)
        {
            if (!IsUserEnabled(user))
            {
                return Task.FromResult(SignInResult.NotAllowed);
            }
            return base.PasswordSignInAsync(user, password, isPersistent, lockoutOnFailure);
        }

        public override Task<bool> ValidateSecurityStampAsync(ApplicationUser? user, string? securityStamp)
        {
            if (!IsUserEnabled(user))
            {
                return Task.FromResult<bool>(false);
            }

            return base.ValidateSecurityStampAsync(user, securityStamp);
        }

        public override Task<bool> CanSignInAsync(ApplicationUser user)
        {
            if (!IsUserEnabled(user))
            {
                return Task.FromResult<bool>(false);
            }

            return base.CanSignInAsync(user);
        }

        private static bool IsUserEnabled(ApplicationUser? user)
        {
            if (user is null)
            {
                return true;
            }
            return user.IsEnabled;
        }
    }
}