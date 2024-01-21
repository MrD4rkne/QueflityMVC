using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.User
{
    public record ListUsersVM
    {
        public required PaginationVM<UserForListVM> Pagination { get; set; }

        public string? UserNameFilter { get; set; }
    }
}