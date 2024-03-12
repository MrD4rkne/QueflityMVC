using QueflityMVC.Application.ViewModels.User;

namespace QueflityMVC.Application.Interfaces;

public interface IUserService
{
    Task DisableUserAsync(string userToDisableId);

    Task EnableUserAsync(string userToEnableId);

    Task<ListUsersVm> GetFilteredListAsync(ListUsersVm listUsersVm);

    Task<UserClaimsVm> GetUsersClaimsVmAsync(string userId);

    Task<UserRolesVm> GetUsersRolesVmAsync(string userId);

    Task UpdateUserClaimsAsync(UserClaimsVm userClaimsVm);

    Task UpdateUserRolesAsync(UserRolesVm userRolesVm);
}