using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Item;

public record ListItemsVm
{
    public required PaginationVm<ItemForListVm> Pagination { get; set; }

    public int? CategoryId { get; set; }

    public string? NameFilter { get; set; }
}