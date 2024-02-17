using QueflityMVC.Application.ViewModels.User;

namespace QueflityMVC.Application.Interfaces;

public interface IUserService
{
    Task DisableUserAsync(string userToDisableId);

    Task EnableUserAsync(string userToEnableId);

    Task<ListUsersVM> GetFilteredListAsync(ListUsersVM listUsersVM);

    Task<UserClaimsVM> GetUsersClaimsVMAsync(string userId);

    Task<UserRolesVM> GetUsersRolesVMAsync(string userId);

    Task UpdateUserClaimsAsync(UserClaimsVM userClaimsVM);

    Task UpdateUserRolesAsync(UserRolesVM userRolesVM);
}