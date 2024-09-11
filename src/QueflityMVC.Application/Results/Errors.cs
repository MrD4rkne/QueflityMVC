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

    public static class Files
    {
        public static readonly Error FileUploadFailed = new(
            ErrorCodes.Files.FILE_UPLOAD_FAILED, "File upload failed");
    }

    public static class Kits
    {
        public static readonly Error DoesNotExit = new(
            ErrorCodes.Kits.DOES_NOT_EXIST, "Kit does not exist");
    }

    public static class Product
    {
        public static readonly Error InvalidOrder = new(
            ErrorCodes.Product.INVALID_ORDER, "Order of product is not valid");

        public static readonly Error ProductMissingInOrder = new(
            ErrorCodes.Product.product_MISSING_IN_ORDER, "Not every visible product is in order");

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

        public static readonly Error AlreadyExists = new(
            ErrorCodes.Conversation.ALREADY_EXISTS, "Conversation already exists");
    }

    public static class Components
    {
        public static readonly Error DoesNotExist = new(
            ErrorCodes.Components.DOES_NOT_EXIST, "Component does not exist");

        public static readonly Error DuplicatedName = new(
            ErrorCodes.Components.DUPLICATED_NAME, "Component with this name already exists");
    }

    public static class Categories
    {
        public static readonly Error DuplicatedName = new(
            ErrorCodes.Categories.DUPLICATED_NAME, "Category with this name already exists");

        public static readonly Error DoesNotExist = new(
            ErrorCodes.Categories.DOES_NOT_EXIST, "Category does not exist");

        public static readonly Error HasItems = new(
            ErrorCodes.Categories.HAS_ITEMS, "Category has items");
    }

    public static class Emails
    {
        public static readonly Error CouldNotSentEmail = new(
            ErrorCodes.Emails.COULD_NOT_SEND, "Could not send email");
    }
}