using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;
    protected Context DbContext;

    public UserRepository(Context dbContext, UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        DbContext = dbContext;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter)
    {
        var matchingUsers = _userManager.Users.AsNoTracking();

        if (!string.IsNullOrEmpty(userNameFilter))
            matchingUsers = matchingUsers.Where(user => user.UserName != null)
                .Where(x => x.UserName!.Contains(userNameFilter));
        return matchingUsers;
    }

    public async Task DisableUserAsync(string userId)
    {
        var userToDisable = await GetUserByIdAsync(userId);
        if (userToDisable is null) throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

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
        var allegedUser = await _userManager.FindByIdAsync(userId);
        return allegedUser is not null;
    }

    public async Task EnableUserAsync(string userToEnableId)
    {
        var userToEnable = await GetUserByIdAsync(userToEnableId) ??
                           throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        userToEnable.IsEnabled = true;
        await _userManager.UpdateAsync(userToEnable);
    }

    public IQueryable<IdentityRole> GetAllRoles()
    {
        var allRoles = DbContext.Roles
            .AsNoTracking();
        return allRoles;
    }

    public async Task<IList<string>> GetAssignedRolesNamesAsync(string userId)
    {
        var rolesOwner = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException();
        var allAssignedRolesIds = await _userManager.GetRolesAsync(rolesOwner);
        return allAssignedRolesIds;
    }

    public async Task AddToRoleAsync(string userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        var role = await GetRoleByIdAsync(roleId) ??
                   throw new ResourceNotFoundException(entityName: nameof(IdentityRole));
        await _userManager.AddToRoleAsync(user, role.Name);
        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task RemoveFromRoleAsync(string userId, string roleId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        var role = await GetRoleByIdAsync(roleId) ??
                   throw new ResourceNotFoundException(entityName: nameof(IdentityRole));
        await _userManager.RemoveFromRoleAsync(user, role.Name);
        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<List<string>> GetAssignedClaimsIdsAsync(string userId)
    {
        var rolesOwner = await GetUserByIdAsync(userId) ?? throw new ResourceNotFoundException();

        var allAssignedClaimsIds = DbContext.UserClaims.Where(x => x.UserId == userId);
        return allAssignedClaimsIds.Select(x => x.ClaimType).Where(x => !string.IsNullOrEmpty(x))
            .ToList()!;
    }

    public async Task GiveClaimsAsync(string userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null) throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

        IEnumerable<Claim> claimsToAdd = claimsIds.AsParallel().Select(cl => { return new Claim(cl, cl); });
        await _userManager.AddClaimsAsync(user, claimsToAdd);
        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task RemoveClaimsAsync(string userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        IEnumerable<Claim> claimsToRemove = claimsIds.AsParallel().Select(cl => { return new Claim(cl, cl); });
        await _userManager.RemoveClaimsAsync(user, claimsToRemove);
        await _userManager.UpdateSecurityStampAsync(user);
    }

    public async Task<bool> HasVerifiedEmail(string userId)
    {
        return await _userManager.IsEmailConfirmedAsync(await GetUserByIdAsync(userId));
    }

    public async Task<string?> GetEmailForUserAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(user=>user.Id==userId) ?? throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        return user.Email;
    }

    private Task<IdentityRole?> GetRoleByIdAsync(string roleId)
    {
        return DbContext.Roles
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == roleId);
    }
}