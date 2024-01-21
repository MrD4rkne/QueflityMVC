using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task DisableUser(string userToDisableId);

        Task<bool> DoesUserExist(string userId);

        Task<ApplicationUser?> GetUserById(string userId);

        Task EnableUser(string userToEnableId);

        IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter);

        IQueryable<IdentityRole> GetAllRoles();

        Task<IList<string>> GetAssignedRolesIds(string userId);

        Task AddToRole(string userId, string roleId);

        Task RemoveFromRole(string userId, string roleId);

        Task<List<string>> GetAssignedClaimsIds(string userId);

        Task GiveClaims(string userId, string[] claimsIds);

        Task RemoveClaims(string userId, string[] claimsIds);
    }
}