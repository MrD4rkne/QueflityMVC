using QueflityMVC.Application.ViewModels.ItemSet;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Item
{
    public record ListItemsForComponentsVM
    {
        public required PaginationVM<ItemForListVM> Pagination { get; set; }

        public int? CategoryId { get; set; }

        public string? NameFilter { get; set; }

        public List<int>? SetsComponentsIds { get; set; }

        public required int SetId { get; set; }

        public ItemSetDetailsVM? SetDetailsVM { get; set; }
    }
}
