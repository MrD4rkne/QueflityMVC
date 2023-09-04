
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.ItemSet
{
    public record ListItemSetsVM
    {
        public required PaginationVM<ItemSetForListVM> Pagination { get; set; }

        public int? CategoryId { get; set; }

        public string? NameFilter { get; set; }
    }
}
