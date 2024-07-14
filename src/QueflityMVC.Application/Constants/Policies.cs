namespace QueflityMVC.Application.Constants;

public static class Policies
{
    public const string USERS_LIST = "CanListUsers";
    public const string USER_CLAIMS_MANAGE = "CanManageClaims";
    public const string USER_CLAIMS_VIEW = "CanViewUserClaims";
    public const string USER_DISABLE = "CanDisableUser";
    public const string USER_ENABLE = "CanEnableUser";
    public const string USER_ROLES_VIEW = "CanViewUserRoles";
    public const string USER_ROLES_MANAGE = "CanManageUserRoles";
    public const string ENTITIES_EDIT = "CanEditEntities";
    public const string ENTITIES_LIST = "CanListEntities";
    public const string ENTITIES_CREATE = "CanCreateEntities";
    public const string ENTITIES_ORDER = "OrderEntities";
}