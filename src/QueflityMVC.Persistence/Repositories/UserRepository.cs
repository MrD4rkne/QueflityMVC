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
        {
            matchingUsers = matchingUsers.Where(user => user.UserName != null)
                .Where(x => x.UserName!.Contains(userNameFilter));
        }

        return matchingUsers;
    }

    public Task<ApplicationUser?> GetUserByIdAsync(Guid userId)
    {
        return userManager.FindByIdAsync(userId.ToString());
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

    public async Task UpdateClaimsAsync(Guid userId, string[] claimsIds)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        }
        
        dbContext.UserClaims.RemoveRange(dbContext.UserClaims.Where(x => x.UserId == userId));
        dbContext.UserClaims.AddRange(claimsIds.Select(claimId => new IdentityUserClaim<Guid>
        {
            UserId = userId,
            ClaimType = claimId,
            ClaimValue = claimId
        }));
        
        await dbContext.SaveChangesAsync();
    }

    public async Task<bool> HasVerifiedEmail(Guid userId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

        return user.EmailConfirmed;
    }

    public async Task<string?> GetEmailForUserAsync(Guid userId)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

        return user.Email;
    }

    public async Task<bool> HasClaimAsync(Guid userId, string claimName, string claimValue)
    {
        var user = await GetUserByIdAsync(userId) ??
                   throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));

        bool hasClaimDirectly = await DbContext.UserClaims.AnyAsync(uc =>
            uc.UserId == user.Id &&
            uc.ClaimType == claimName &&
            uc.ClaimValue == claimValue);
        if (hasClaimDirectly)
        {
            return true;
        }

        bool hasClaimByRole = await DbContext.RoleClaims.AnyAsync(rc =>
            DbContext.UserRoles.Any(ur => ur.UserId == user.Id && ur.RoleId == rc.RoleId) &&
            rc.ClaimType == claimName &&
            rc.ClaimValue == claimValue);
        return hasClaimByRole;
    }

    public async Task UpdateUserRolesAsync(Guid userId, Guid[] rolesForUser)
    {
        var user = await GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
        }
        
        dbContext.UserRoles.RemoveRange(dbContext.UserRoles.Where(x => x.UserId == userId));
        dbContext.UserRoles.AddRange(rolesForUser.Select(claimId => new IdentityUserRole<Guid>()
        {
            UserId = userId,
            RoleId = claimId
        }));
        
        await dbContext.SaveChangesAsync();
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