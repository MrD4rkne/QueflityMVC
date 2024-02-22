namespace QueflityMVC.Application.ViewModels.Component;

public record ListComponentsVM
{
    public required Pagination.PaginationVM<ComponentForListVM> Pagination { get; set; }
    public int? ItemId { get; set; }

    public string? NameFilter { get; set; }
}