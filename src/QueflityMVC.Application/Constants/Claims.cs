namespace QueflityMVC.Application.Constants;

public static class Claims
{
    public const string USER_CLAIMS_MANAGE = "ManageUserClaims";
    public const string USER_CLAIMS_VIEW = "ListUserClaims";
    public const string USERS_LIST = "ListUsers";
    public const string USER_DISABLE = "DisableUser";
    public const string USER_ENABLE = "EnableUser";
    public const string USER_ROLES_LIST = "ListUserRoles";
    public const string USER_ROLES_MANAGE = "ManageUserRoles";
    public const string ENTITIES_EDIT = "EditEntities";
    public const string ENTITIES_LIST = "ListEntities";
    public const string ENTITIES_CREATE = "CreateEntities";
    public const string ENTITIES_ORDER = "OrderEntities";

    public static IEnumerable<string> GetAll()
    {
        return
        [
            USER_CLAIMS_MANAGE,
            USER_CLAIMS_VIEW,
            USERS_LIST,
            USER_DISABLE,
            USER_ENABLE,
            USER_ROLES_LIST,
            USER_ROLES_MANAGE,
            ENTITIES_EDIT,
            ENTITIES_CREATE,
            ENTITIES_LIST,
            ENTITIES_ORDER
        ];
    }
}