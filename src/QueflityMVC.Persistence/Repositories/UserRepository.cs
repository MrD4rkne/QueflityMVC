using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Persistence.Repositories;

public class UserRepository(
    Context dbContext,
    UserManager<ApplicationUser> userManager,
    RoleManager<ApplicationRole> roleManager)
    : IUserRepository
{
    private readonly RoleManager<ApplicationRole> _roleManager = roleManager;

    protected Context DbContext = dbContext;

    public IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter)
    {
        var matchingUsers = userManager.Users.AsNoTracking();

        if (!string.IsNullOrEmpty(userNameFilter))
            matchingUsers = matchingUsers.Where(user => user.UserName != null)
                .Where(x => x.UserName!.Contains(userNameFilter));
        return matchingUsers;
    }

    public Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
    {
        return userManager.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == userId);
    }

    public async Task<bool> DoesUserExistAsync(Guid userId)
    {
        var allegedUser = await GetUserByIdAsync(userId);
        return allegedUser is not null;
    }

    public IQueryable<ApplicationRole> GetAllRoles()
    {
        var allRoles = DbContext.Roles
            .AsNoTracking();
        return allRoles;
    }

    public async Task<IList<string>> GetAssignedRolesNamesAsync(Guid userId)
    {
        var rolesOwner = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException();
        var allAssignedRolesIds = await userManager.GetRolesAsync(rolesOwner);
        return allAssignedRolesIds;
    }

    public async Task AddToRoleAsync(Guid userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        var role = await GetRoleByIdAsync(roleId) ??
                   throw new ResourceNotFoundException(entityName: nameof(IdentityRole));
        await userManager.AddToRoleAsync(user, role.Name);
        await userManager.UpdateSecurityStampAsync(user);
    }

    public async Task RemoveFromRoleAsync(Guid userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        var role = await GetRoleByIdAsync(roleId) ??
                   throw new ResourceNotFoundException(entityName: nameof(IdentityRole));
        await userManager.RemoveFromRoleAsync(user, role.Name);
        await userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<List<string>> GetAssignedClaimsIdsAsync(Guid userId)
    {
        var rolesOwner = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException();

        var allAssignedClaimsIds = DbContext.UserClaims.Where(x => x.UserId == userId);
        return allAssignedClaimsIds.Select(x => x.ClaimType).Where(x => !string.IsNullOrEmpty(x))
            .ToList()!;
    }

    public async Task GiveClaimsAsync(Guid userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null) throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

        IEnumerable<Claim> claimsToAdd = claimsIds.AsParallel().Select(cl => { return new Claim(cl, cl); });
        await userManager.AddClaimsAsync(user, claimsToAdd);
        await userManager.UpdateSecurityStampAsync(user);
    }

    public async Task RemoveClaimsAsync(Guid userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        IEnumerable<Claim> claimsToRemove = claimsIds.AsParallel().Select(cl => new Claim(cl, cl));
        await userManager.RemoveClaimsAsync(user, claimsToRemove);
        await userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<bool> HasVerifiedEmail(Guid userId)
    {
        return await userManager.IsEmailConfirmedAsync(await GetUserByIdAsync(userId));
    }

    public async Task<string?> GetEmailForUserAsync(Guid userId)
    {
        var user = await userManager.Users.FirstAsync(user => user.Id == userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        return user.Email;
    }

    public Task<bool> CanRespondToConversations(Guid userId)
    {
        return Task.FromResult(true);
    }

    public async Task UpdateAsync(ApplicationUser userToUpdate)
    {
        var user = await DbContext.Users.FirstOrDefaultAsync(user => user.Id == userToUpdate.Id);
        user.UserName = userToUpdate.UserName;
        user.IsEnabled = userToUpdate.IsEnabled;
        await DbContext.SaveChangesAsync();
    }

    private Task<ApplicationRole?> GetRoleByIdAsync(string roleId)
    {
        return roleManager.FindByIdAsync(roleId);
    }
}