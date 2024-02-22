using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IUserRepository
{
    Task DisableUserAsync(string userToDisableId);

    Task<bool> DoesUserExistAsync(string userId);

    Task<ApplicationUser?> GetUserByIdAsync(string userId);

    Task EnableUserAsync(string userToEnableId);

    IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter);

    IQueryable<IdentityRole> GetAllRoles();

    Task<IList<string>> GetAssignedRolesNamesAsync(string userId);

    Task AddToRoleAsync(string userId, string roleId);

    Task RemoveFromRoleAsync(string userId, string roleId);

    Task<List<string>> GetAssignedClaimsIdsAsync(string userId);

    Task GiveClaimsAsync(string userId, string[] claimsIds);

    Task RemoveClaimsAsync(string userId, string[] claimsIds);
}