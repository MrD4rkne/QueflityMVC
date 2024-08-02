using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IUserRepository
{
    Task<bool> DoesUserExistAsync(Guid userId);

    Task<ApplicationUser?> GetUserByIdAsync(Guid userId);

    Task UpdateAsync(ApplicationUser userToUpdate);

    IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter);

    IQueryable<ApplicationRole> GetAllRoles();

    Task<IList<string>> GetAssignedRolesNamesAsync(Guid userId);

    Task AddToRoleAsync(Guid userId, string roleId);

    Task RemoveFromRoleAsync(Guid userId, string roleId);

    Task<List<string>> GetAssignedClaimsIdsAsync(Guid userId);

    Task GiveClaimsAsync(Guid userId, string[] claimsIds);

    Task RemoveClaimsAsync(Guid userId, string[] claimsIds);

    Task<bool> HasVerifiedEmail(Guid userId);

    Task<string?> GetEmailForUserAsync(Guid userId);

    Task<bool> CanRespondToConversations(Guid userId);
}