using Microsoft.AspNetCore.Authorization;
using System.Reflection.Metadata.Ecma335;

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

            options.AddPolicy("CanManageClaims", policy =>
                policy.RequireClaim("ManageClaims", "ViewUserClaims"));
            options.AddPolicy("CanListUsers", policy =>
                policy.RequireClaim("ListUsers","ListUsers"));
            options.AddPolicy("CanViewUserClaims", policy =>
                policy.RequireClaim("ViewUserClaims"));
            options.AddPolicy("CanDisableUser", policy =>
                policy.RequireClaim("DisableUser"));
            options.AddPolicy("CanEnableUser", policy =>
                policy.RequireClaim("EnableUser"));

            return options;
        }
    }
}
