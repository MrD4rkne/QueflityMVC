using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Exceptions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.Services;

public class UserService(IUserRepository userRepository, IMapper mapper, IUserContext userContext) : IUserService
{
    public async Task<Result> DisableUserAsync(Guid userToDisableId)
    {
        if (userContext.UserId == userToDisableId)
        {
            return Result.Failure(Errors.User.CannotManageThemselves);
        }

        var user = await userRepository.GetUserByIdAsync(userToDisableId);
        if (user is null)
        {
            return Result.Failure(Errors.User.DoesNotExist);
        }

        user.IsEnabled = false;

        await userRepository.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> EnableUserAsync(Guid userToEnableId)
    {
        if (userContext.UserId == userToEnableId)
        {
            return Result.Failure(Errors.User.CannotManageThemselves);
        }

        var user = await userRepository.GetUserByIdAsync(userToEnableId);
        if (user is null)
        {
            return Result.Failure(Errors.User.DoesNotExist);
        }

        user.IsEnabled = true;

        await userRepository.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<ListUsersVm> GetFilteredListAsync(ListUsersVm listUsersVm)
    {
        var matchingUsers = userRepository.GetFilteredUsers(listUsersVm.UserNameFilter)
            .OrderBy(user => user.Id);
        listUsersVm.Pagination = await matchingUsers.Paginate(listUsersVm.Pagination, mapper.ConfigurationProvider);

        return listUsersVm;
    }

    public async Task<UserClaimsVm> GetUsersClaimsVmAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var allClaims = Claims.GetAll()
            .Select(str => new ClaimForSelectionVm(str));
        var assignedClaimsIds = await userRepository.GetAssignedClaimsIdsAsync(userId);

        UserClaimsVm userClaimsVm = new()
        {
            UserId = userId,
            Username = user.UserName,
            IsEnabled = user.IsEnabled,
            AllClaims = allClaims.ToList(),
            AssignedClaimsIds = assignedClaimsIds.ToList()
        };
        return userClaimsVm;
    }

    public async Task<UserRolesVm> GetUsersRolesVmAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var allRoles = userRepository.GetAllRoles()
            .ProjectTo<RoleForSelectionVm>(mapper.ConfigurationProvider);

        var assignedRolesIds = await userRepository.GetAssignedRolesNamesAsync(userId);

        UserRolesVm userRolesVm = new()
        {
            UserId = userId,
            Username = user.UserName,
            IsEnabled = user.IsEnabled,
            AllRoles = allRoles.ToList(),
            AssignedRolesNames = assignedRolesIds.ToList()
        };
        return userRolesVm;
    }

    public async Task<Result> UpdateUserClaimsAsync(UserClaimsVm userClaimsVm)
    {
        string[] claimsToGive = userClaimsVm.AllClaims
            .Where(x => x.IsSelected)
            .Select(x => x.Id)
            .Distinct()
            .ToArray();

        var allClaims = Claims.GetAll();
        
        bool doAllClaimsExist = claimsToGive.All(claim => allClaims.Contains(claim));
        if (!doAllClaimsExist)
        {
            return Result.Failure(Errors.Claims.DoesNotExist);
        }
        
        await userRepository.UpdateClaimsAsync(userClaimsVm.UserId, claimsToGive);
        return Result.Success();
    }

    public async Task<Result> UpdateUserRolesAsync(UserRolesVm userRolesVm)
    {
        var rolesForUser = userRolesVm.AllRoles
            .Where(x => x.IsSelected)
            .Select(x => new
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToArray();
        
       var allRoles = await userRepository.GetAllRoles()
           .ToListAsync();
       
       bool doAllRolesExist = rolesForUser.All(role => allRoles.Exists(x => x.Id == role.Id));
       if (!doAllRolesExist)
       {
           return Result.Failure(Errors.Roles.DoesNotExist);
       }

       Guid[] rolesToAddIds = rolesForUser.Select(x => x.Id).ToArray();
       await userRepository.UpdateUserRolesAsync(userRolesVm.UserId, rolesToAddIds);
       return Result.Success();
    }
    
}