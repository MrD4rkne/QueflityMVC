using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Item;

public record ListItemsForComponentsVM
{
    public required PaginationVM<ItemForListVM> Pagination { get; set; }

    public int? CategoryId { get; set; }

    public string? NameFilter { get; set; }

    public List<int>? KitComponentsIds { get; set; }

    public required int kitId { get; set; }

    public KitDetailsVM? KitDetailsVM { get; set; }
}