using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Web.Chat;

public class CookiesHubValidationFilter(SignInManager<ApplicationUser> signInManager) : IHubFilter
{
    public async ValueTask<object> InvokeMethodAsync(HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object>> next)
    {
        // if (!signInManager.IsSignedIn(invocationContext.Context.User))
        // {
        //     invocationContext.Context.Abort();
        //     throw new NotImplementedException();
        // }
        return next(invocationContext);
    }
}