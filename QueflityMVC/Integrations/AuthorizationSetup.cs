using Microsoft.AspNetCore.Authorization;
using QueflityMVC.Web.Constants;

namespace QueflityMVC.Web.Integrations
{
    public static class AuthorizationSetup
    {
        public static AuthorizationOptions AddPolicies(this AuthorizationOptions options)
        {
            //options.AddPolicy("CanManageIngredients", policy =>
            //        policy.RequireClaim("AddIngredient", "EditIngredient"));
            //options.AddPolicy("CanViewIngredients", policy =>
            //    policy.RequireClaim("CanViewIngredients"));
            //options.AddPolicy("CanDeleteIngredients", policy =>
            //    policy.RequireClaim("CanDeleteIngredients"));

            //options.AddPolicy("CanManageItems", policy =>
            //    policy.RequireClaim("AddItems", "EditItems"));
            //options.AddPolicy("CanViewItems", policy =>
            //    policy.RequireClaim("CanViewItems"));
            //options.AddPolicy("CanDeleteItems", policy =>
            //    policy.RequireClaim("CanDeleteItems"));

            //options.AddPolicy("CanManageCategories", policy =>
            //    policy.RequireClaim("AddCategories", "EditCategories"));
            //options.AddPolicy("CanViewCategories", policy =>
            //    policy.RequireClaim("CanViewCategories"));
            //options.AddPolicy("CanDeleteCategories", policy =>
            //    policy.RequireClaim("CanDeleteCategories"));

            //options.AddPolicy("CanManageKits", policy =>
            //    policy.RequireClaim("CanManageKits", "AddKits", "EditKits"));
            //options.AddPolicy("CanViewKits", policy =>
            //    policy.RequireClaim("CanViewKits"));
            //options.AddPolicy("CanDeleteKits", policy =>
            //    policy.RequireClaim("CanDeleteKits", "DeleteKits"));
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

            return options;
        }
    }
}
