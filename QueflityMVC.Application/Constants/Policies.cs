namespace QueflityMVC.Application.Constants
{
    public static class Policies
    {
        public const string USERS_LIST = "CanListUsers";
        public const string USER_CLAIMS_MANAGE = "CanManageClaims";
        public const string USER_CLAIMS_VIEW = "CanViewUserClaims";
        public const string USER_DISABLE = "CanDisableUser";
        public const string USER_ENABLE = "CanEnableUser";
        public const string USER_ROLES_VIEW = "CanViewUserRoles";
        public const string USER_ROLES_MANAGE = "CanManageUserRoles";
    }

    public static class Claims
    {
        public const string USER_CLAIMS_MANAGE = "ManageUserClaims";
        public const string USER_CLAIMS_VIEW = "ListUserClaims";
        public const string USERS_LIST = "ListUsers";
        public const string USER_DISABLE = "DisableUser";
        public const string USER_ENABLE = "EnableUser";
        public const string USER_ROLES_LIST = "ListUserRoles";
        public const string USER_ROLES_MANAGE = "ManageUserRoles";

        public static List<string> GetAll()
        {
            return new List<string>()
            {
                USER_CLAIMS_MANAGE,
                USER_CLAIMS_VIEW,
                USERS_LIST,
                USER_DISABLE,
                USER_ENABLE,
                USER_ROLES_LIST,
                USER_ROLES_MANAGE
            };
        }
    }
}
