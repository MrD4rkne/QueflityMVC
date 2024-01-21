using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Category
{
    public record ListCategoriesVM
    {
        public required PaginationVM<CategoryForListVM> Pagination { get; set; }

        public string? NameFilter { get; set; }
    }
}