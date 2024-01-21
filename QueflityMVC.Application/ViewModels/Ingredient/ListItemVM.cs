namespace QueflityMVC.Application.ViewModels.Ingredient
{
    public record ListIngredientsVM
    {
        public required Pagination.PaginationVM<IngredientForListVM> Pagination { get; set; }
        public int? ItemId { get; set; }

        public string? NameFilter { get; set; }
    }
}