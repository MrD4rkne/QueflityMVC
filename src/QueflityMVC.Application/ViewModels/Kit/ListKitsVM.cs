using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Kit;

public record ListKitsVm
{
    public required PaginationVm<KitForListVm> Pagination { get; set; }

    public int? ItemId { get; set; }

    public string? NameFilter { get; set; }
}