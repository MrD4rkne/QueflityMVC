using Microsoft.AspNetCore.Authorization;

namespace QueflityMVC.Web.Setup.Identity;

public class OneOfMultiplePoliciesRequirement(params string[] policies) : IAuthorizationRequirement
{
    public string[] Policies { get; } = policies;
}