namespace QueflityMVC.Application.ViewModels.ItemCategory
{
    public class ListItemCategoriesVM
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public List<ItemCategoryForListVM> ItemCategories { get; set; }

        public string NameFilter { get; set; }
    }
}
