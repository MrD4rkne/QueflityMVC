using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Component;

public record ListComponentsVm
{
    public required PaginationVm<ComponentForListVm> Pagination { get; set; }
    public int? ItemId { get; set; }

    public string? NameFilter { get; set; }
}