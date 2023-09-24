using QueflityMVC.Application.ViewModels.User;

namespace QueflityMVC.Application.Interfaces
{
    public interface IUserService
    {
        Task DisableUser(string userToDisableId);

        Task EnableUser(string userToEnableId);

        Task<ListUsersVM> GetFilteredList(ListUsersVM listUsersVM);

        Task<UserRolesVM> GetUsersRolesVM(string userId);

        Task UpdateUserRoles(UserRolesVM userRolesVM);
    }
}
