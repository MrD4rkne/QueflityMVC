namespace QueflityMVC.Application.Results;

public static class Errors
{
    public static class Items
    {
        public static readonly Error DoesNotExit = new(
            ErrorCodes.Items.DOES_NOT_EXIST, "Item does not exist");

        public static readonly Error IsPartOfKit = new(
            ErrorCodes.Items.IS_PART_OF_KIT, "Item belongs to kit");

        public static readonly Error NoCategories = new(
            ErrorCodes.Items.NO_CATEGORIES, "No categories found");
    }

    public static class Kits
    {
        public static readonly Error DoesNotExit = new(
            ErrorCodes.Kits.DOES_NOT_EXIST, "Kit does not exist");
    }

    public static class Product
    {
        public static readonly Error InvalidOrder = new(
            ErrorCodes.Product.INVALID_ORDER, "Order of purchasable is not valid");

        public static readonly Error ProductMissingInOrder = new(
            ErrorCodes.Product.PURCHASABLE_MISSING_IN_ORDER, "Not every visible purchasable is in order");

        public static readonly Error DoesNotExist = new(
            ErrorCodes.Product.DOES_NOT_EXIST, "Product does not exist");
    }

    public static class User
    {
        public static readonly Error EmailNotVerified = new(
            ErrorCodes.User.EMAIL_NOT_VERIFIED, "Email is not verified");

        public static readonly Error CannotManageThemselves = new(
            ErrorCodes.User.CANNOT_MANAGE_SELF, "User cannot manage themselves");

        public static readonly Error DoesNotExist = new(
            ErrorCodes.User.DOES_NOT_EXIST, "User does not exist");
    }

    public static class Conversation
    {
        public static readonly Error DoesNotExist = new(
            ErrorCodes.Conversation.DOES_NOT_EXIST, "Conversation does not exist");

        public static readonly Error DoesNotBelongToUser = new(
            ErrorCodes.Conversation.DOES_NOT_BELONG_TO_USER, "Conversation does not belong to user");
    }
}