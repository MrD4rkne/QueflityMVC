namespace QueflityMVC.Web.Constants
{
    internal static class Policies
    {
        public const string USERS_LIST = "CanListUsers";
        public const string USER_CLAIMS_MANAGE = "CanManageClaims";
        public const string USER_CLAIMS_VIEW = "CanViewUserClaims";
        public const string USER_DISABLE = "CanDisableUser";
        public const string USER_ENABLE = "CanEnableUser";
        public const string USER_ROLES_VIEW = "CanViewUserRoles";
        public const string USER_ROLES_MANAGE = "CanManageUserRoles";
    }

    internal static class Claims
    {
        public const string USER_CLAIMS_MANAGE = "ManageUserClaims";
        public const string USER_CLAIMS_VIEW = "ListUserClaims";
        public const string USERS_LIST = "ListUsers";
        public const string USER_DISABLE = "DisableUser";
        public const string USER_ENABLE = "EnableUser";
        public const string USER_ROLES_LIST = "ListUserRoles";
        public const string USER_ROLES_MANAGE = "ManageUserRoles";
    }
}
