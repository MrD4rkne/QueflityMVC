using Microsoft.AspNetCore.Authorization;

namespace QueflityMVC.Web.Setup.Identity;

public class OneOfMultiplePoliciesAuthorizationHandler(IServiceProvider serviceProvider)
    : AuthorizationHandler<OneOfMultiplePoliciesRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OneOfMultiplePoliciesRequirement requirement)
    {
        var authorizationService = serviceProvider.GetRequiredService<IAuthorizationService>();
        foreach (string policy in requirement.Policies)
        {
            var result = await authorizationService.AuthorizeAsync(context.User, policy);
            if (result.Succeeded)
            {
                context.Succeed(requirement); // User meets one of the policies
                return;
            }
        }

        context.Fail(); // None of the policies were fulfilled
    }
}