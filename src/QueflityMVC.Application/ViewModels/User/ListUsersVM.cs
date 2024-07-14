using QueflityMVC.Application.ViewModels.Pagination;

namespace QueflityMVC.Application.ViewModels.User;

public record ListUsersVm
{
    public required PaginationVm<UserForListVm> Pagination { get; set; }

    public string? UserNameFilter { get; set; }
}