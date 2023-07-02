
namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public class ListItemSetsVM
    {
        public int PageSize { get; set; }

        public int PageIndex { get; set; }

        public int TotalCount { get; set; }

        public int? ItemCategoryId { get; set; }

        public List<ItemSetForListVM> Items { get; set; }

        public string NameFilter { get; set; }
    }
}
