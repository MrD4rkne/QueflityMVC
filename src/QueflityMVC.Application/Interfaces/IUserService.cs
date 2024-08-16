using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.User;

namespace QueflityMVC.Application.Interfaces;

public interface IUserService
{
    Task<Result> DisableUserAsync(Guid userToDisableId);

    Task<Result> EnableUserAsync(Guid userToEnableId);

    Task<ListUsersVm> GetFilteredListAsync(ListUsersVm listUsersVm);

    Task<UserClaimsVm> GetUsersClaimsVmAsync(Guid userId);

    Task<UserRolesVm> GetUsersRolesVmAsync(Guid userId);

    Task UpdateUserClaimsAsync(UserClaimsVm userClaimsVm);

    Task UpdateUserRolesAsync(UserRolesVm userRolesVm);
}