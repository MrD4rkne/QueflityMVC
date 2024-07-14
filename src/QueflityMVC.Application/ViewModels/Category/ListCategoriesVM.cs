using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Category;

public record ListCategoriesVm
{
    public required PaginationVm<CategoryForListVm> Pagination { get; set; }

    public string? NameFilter { get; set; }
}