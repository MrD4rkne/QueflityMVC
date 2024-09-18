using Microsoft.AspNetCore.Authorization;

namespace QueflityMVC.Web.Setup.Identity;

public class OneOfMultiplePoliciesAuthorizationHandler(IServiceProvider serviceProvider)
    : AuthorizationHandler<OneOfMultiplePoliciesRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OneOfMultiplePoliciesRequirement requirement)
    {
        var authorizationService = serviceProvider.GetRequiredService<IAuthorizationService>();

        // Parallelize sub-policy checks
        var tasks = requirement.Policies.Select(async policy =>
        {
            var result = await authorizationService.AuthorizeAsync(context.User, policy);
            return result.Succeeded;
        });

        // Wait for all tasks to complete and check if any task returned true
        var results = await Task.WhenAll(tasks);

        // If any task succeeded, return true
        if (results.Any(r => r))
        {
            context.Succeed(requirement);
            return;
        }
        
        context.Fail();
    }
}