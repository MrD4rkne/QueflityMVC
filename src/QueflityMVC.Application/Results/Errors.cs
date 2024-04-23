namespace QueflityMVC.Application.Results;

public static class Errors
{
    public static class Items
    {
        public static readonly Error DoesNotExit = new(
            "Item.DoesNotExist", "Item does not exist");

        public static readonly Error IsPartOfKit = new(
            "Item.IsPartOfKit", "Item belongs to kit");

        public static readonly Error NoCategories = new(
            "Item.NoCategories", "No categories found");
    }

    public static class Kits
    {
        public static readonly Error DoesNotExit = new(
            "Kit.DoesNotExist", "Kit does not exist");
    }

    public static class Purchasable
    {
        public static readonly Error InvalidOrder = new(
            "Purchasable.NotValidOrder", "Order of purchasable is not valid");

        public static readonly Error PurchasableMissingInOrder = new(
            "Purchasable.PurchasableMissingInOrder", "Not every visible purchasable is in order");
    }
}