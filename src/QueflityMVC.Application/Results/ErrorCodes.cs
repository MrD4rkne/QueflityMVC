namespace QueflityMVC.Application.Results;

public static class ErrorCodes
{
    public static class Items
    {
        public const string DOES_NOT_EXIST = "Item.DoesNotExist";
        public const string IS_PART_OF_KIT = "Item.IsPartOfKit";
        public const string NO_CATEGORIES = "Item.NoCategories";
    }

    public static class Kits
    {
        public const string DOES_NOT_EXIST = "Kit.DoesNotExist";
    }

    public static class Purchasable
    {
        public const string DOES_NOT_EXIST = "Purchasable.DoesNotExist";
        public const string INVALID_ORDER = "Purchasable.NotValidOrder";
        public const string PURCHASABLE_MISSING_IN_ORDER = "Purchasable.PurchasableMissingInOrder";
    }

    public static class User
    {
        public const string EMAIL_NOT_VERIFIED = "User.EmailNotVerified";
    }
}