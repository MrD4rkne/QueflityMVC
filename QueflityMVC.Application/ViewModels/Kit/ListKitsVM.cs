using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.Kit
{
    public record ListKitsVM
    {
        public required PaginationVM<KitForListVM> Pagination { get; set; }

        public int? CategoryId { get; set; }

        public string? NameFilter { get; set; }
    }
}