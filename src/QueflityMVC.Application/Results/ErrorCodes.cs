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

    public static class Product
    {
        public const string DOES_NOT_EXIST = "Product.DoesNotExist";
        public const string INVALID_ORDER = "Product.NotValidOrder";
        public const string PURCHASABLE_MISSING_IN_ORDER = "Product.ProductMissingInOrder";
    }

    public static class User
    {
        public const string EMAIL_NOT_VERIFIED = "User.EmailNotVerified";
    }
}