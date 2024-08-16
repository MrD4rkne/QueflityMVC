using Microsoft.AspNetCore.Authorization;
using QueflityMVC.Application.Constants;

namespace QueflityMVC.Web.Setup.Identity;

public static class AuthorizationSetup
{
    public static AuthorizationOptions AddPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(Policies.USER_CLAIMS_VIEW, policy =>
            policy.RequireClaim(Claims.USER_CLAIMS_VIEW));
        options.AddPolicy(Policies.USER_CLAIMS_MANAGE, policy =>
            policy.RequireClaim(Claims.USER_CLAIMS_MANAGE)
                .RequireClaim(Claims.USER_CLAIMS_VIEW));

        options.AddPolicy(Policies.USERS_LIST, policy =>
            policy.RequireClaim(Claims.USERS_LIST));

        options.AddPolicy(Policies.USER_DISABLE, policy =>
            policy.RequireClaim(Claims.USER_DISABLE));
        options.AddPolicy(Policies.USER_ENABLE, policy =>
            policy.RequireClaim(Claims.USER_ENABLE));

        options.AddPolicy(Policies.USER_ROLES_VIEW, policy =>
            policy.RequireClaim(Claims.USER_ROLES_LIST));
        options.AddPolicy(Policies.USER_ROLES_MANAGE, policy =>
            policy.RequireClaim(Claims.USER_ROLES_MANAGE)
                .RequireClaim(Claims.USER_ROLES_LIST));

        options.AddPolicy(Policies.ENTITIES_LIST, policy =>
            policy.RequireClaim(Claims.ENTITIES_LIST));
        options.AddPolicy(Policies.ENTITIES_EDIT, policy =>
            policy.RequireClaim(Claims.ENTITIES_EDIT)
                .RequireClaim(Claims.ENTITIES_LIST));
        options.AddPolicy(Policies.ENTITIES_CREATE, policy =>
            policy.RequireClaim(Claims.ENTITIES_CREATE)
                .RequireClaim(Claims.ENTITIES_LIST));
        options.AddPolicy(Policies.ENTITIES_ORDER, policy =>
            policy.RequireClaim(Claims.ENTITIES_ORDER)
                .RequireClaim(Claims.ENTITIES_LIST));

        options.AddPolicy(Policies.CONVERSATIONS_RESPOND, policy =>
            policy.RequireClaim(Claims.CONVERSATIONS_RESPOND));

        options.AddPolicy(Policies.SEE_ADMIN_PANEL, policy =>
            policy.Requirements.Add(new OneOfMultiplePoliciesRequirement(Policies.USERS_LIST, Policies.ENTITIES_LIST,
                Policies.CONVERSATIONS_RESPOND, Policies.ENTITIES_ORDER)));

        return options;
    }
}