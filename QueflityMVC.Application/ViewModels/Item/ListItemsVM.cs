using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Item
{
    public record ListItemsVM
    {
        public required PaginationVM<ItemForListVM> Pagination { get; set; }

        public int? CategoryId { get; set; }

        public string? NameFilter { get; set; }
    }
}