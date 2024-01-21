using System.Security.Claims;

namespace QueflityMVC.Web.Common
{
    public static class IdentityExtensions
    {
        public static string? GetLoggedInUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirstValue(ClaimTypes.NameIdentifier);
            return loggedInUserId;
        }
    }
}