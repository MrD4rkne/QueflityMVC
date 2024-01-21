using Microsoft.AspNetCore.Authorization;
using QueflityMVC.Application.Constants;

namespace QueflityMVC.Web.Setup.Identity
{
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

            return options;
        }
    }
}