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

    public static class Purchasable
    {
        public static readonly Error InvalidOrder = new(
            ErrorCodes.Purchasable.INVALID_ORDER, "Order of purchasable is not valid");

        public static readonly Error PurchasableMissingInOrder = new(
            ErrorCodes.Purchasable.PURCHASABLE_MISSING_IN_ORDER, "Not every visible purchasable is in order");
    }
}