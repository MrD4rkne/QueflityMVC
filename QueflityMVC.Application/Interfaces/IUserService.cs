using QueflityMVC.Application.ViewModels.User;

namespace QueflityMVC.Application.Interfaces
{
    public interface IUserService
    {
        Task DisableUser(string userToDisableId);

        Task EnableUser(string userToEnableId);

        Task<ListUsersVM> GetFilteredList(ListUsersVM listUsersVM);

        Task<UserClaimsVM> GetUsersClaimsVM(string userId);

        Task<UserRolesVM> GetUsersRolesVM(string userId);

        Task UpdateUserClaims(UserClaimsVM userClaimsVM);

        Task UpdateUserRoles(UserRolesVM userRolesVM);
    }
}