using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    protected Context _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRepository(Context dbContext, UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter)
    {
        IQueryable<ApplicationUser> matchingUsers = _userManager.Users;

        if (!string.IsNullOrEmpty(userNameFilter))
        {
            matchingUsers = matchingUsers.Where(user => user.UserName != null)
                .Where(x => x.UserName!.Contains(userNameFilter));
        }
        return matchingUsers;
    }

    public async Task DisableUserAsync(string userId)
    {
        var userToDisable = await GetUserByIdAsync(userId);
        if (userToDisable is null)
        {
            throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        }

        userToDisable.IsEnabled = false;
        await _userManager.UpdateAsync(userToDisable);
        await _userManager.UpdateSecurityStampAsync(userToDisable);
    }

    public Task<ApplicationUser?> GetUserByIdAsync(string userId)
    {
        return _userManager.FindByIdAsync(userId);
    }

    public async Task<bool> DoesUserExistAsync(string userId)
    {
        ApplicationUser? allegedUser = await _userManager.FindByIdAsync(userId);
        return allegedUser is not null;
    }

    public async Task EnableUserAsync(string userToEnableId)
    {
        var userToEnable = await GetUserByIdAsync(userToEnableId) ?? throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        userToEnable.IsEnabled = true;
        await _userManager.UpdateAsync(userToEnable);
    }

    public IQueryable<IdentityRole> GetAllRoles()
    {
        var allRoles = _dbContext.Roles;
        return allRoles;
    }

    public async Task<IList<string>> GetAssignedRolesIdsAsync(string userId)
    {
        var rolesOwner = (await GetUserByIdAsync(userId)) ?? throw new ResourceNotFoundException();
        var allAssignedRolesIds = await _userManager.GetRolesAsync(rolesOwner);
        return allAssignedRolesIds;
    }

    public async Task AddToRoleAsync(string userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        await _userManager.AddToRoleAsync(user, roleId);
    }

    public async Task RemoveFromRoleAsync(string userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        }

        await _userManager.RemoveFromRoleAsync(user, roleId);
    }

    public async Task<List<string>> GetAssignedClaimsIdsAsync(string userId)
    {
        var rolesOwner = (await GetUserByIdAsync(userId)) ?? throw new ResourceNotFoundException();

        var allAssignedClaimsIds = _dbContext.UserClaims.Where(x => x.UserId == userId);
        return allAssignedClaimsIds.Select(x => x.ClaimType).Where(x => !string.IsNullOrEmpty(x))
            .ToList()!;
    }

    public async Task GiveClaimsAsync(string userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        }

        IEnumerable<Claim> claimsToAdd = ParallelEnumerable.Select(claimsIds.AsParallel(), (string cl) => { return new Claim(cl, cl); });
        await _userManager.AddClaimsAsync(user, claimsToAdd);
    }

    public async Task RemoveClaimsAsync(string userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        IEnumerable<Claim> claimsToRemove = ParallelEnumerable.Select(claimsIds.AsParallel(), 
            (string cl) => 
            { 
                return new Claim(cl, cl); 
            });
        await _userManager.RemoveClaimsAsync(user, claimsToRemove);
    }
}