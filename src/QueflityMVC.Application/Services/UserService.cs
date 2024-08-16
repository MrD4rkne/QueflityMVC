using AutoMapper;
using AutoMapper.QueryableExtensions;
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
            return Result.Failure(Errors.User.CannotManageThemselves);

        var user = await userRepository.GetUserByIdAsync(userToDisableId);
        if (user is null)
            return Result.Failure(Errors.User.DoesNotExist);
        user.IsEnabled = false;

        await userRepository.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<Result> EnableUserAsync(Guid userToEnableId)
    {
        if (userContext.UserId == userToEnableId)
            return Result.Failure(Errors.User.CannotManageThemselves);

        var user = await userRepository.GetUserByIdAsync(userToEnableId);
        if (user is null)
            return Result.Failure(Errors.User.DoesNotExist);
        user.IsEnabled = true;

        await userRepository.UpdateAsync(user);
        return Result.Success();
    }

    public async Task<ListUsersVm> GetFilteredListAsync(ListUsersVm listUsersVm)
    {
        var matchingUsers = userRepository.GetFilteredUsers(listUsersVm.UserNameFilter);
        listUsersVm.Pagination = await matchingUsers.Paginate(listUsersVm.Pagination, mapper.ConfigurationProvider);

        return listUsersVm;
    }

    public async Task<UserClaimsVm> GetUsersClaimsVmAsync(Guid userId)
    {
        var user = await userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new EntityNotFoundException();

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
        if (user is null) throw new EntityNotFoundException();

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

    public async Task UpdateUserClaimsAsync(UserClaimsVm userClaimsVm)
    {
        var claimsToGive = userClaimsVm.AllClaims
            .Where(x => x.IsSelected)
            .Select(x => x.Id)
            .ToArray();
        var claimsToRemove = userClaimsVm.AllClaims
            .Where(x => x.IsSelected == false)
            .Select(x => x.Id)
            .ToArray();

        await userRepository.GiveClaimsAsync(userClaimsVm.UserId, claimsToGive);
        await userRepository.RemoveClaimsAsync(userClaimsVm.UserId, claimsToRemove);
    }

    public async Task UpdateUserRolesAsync(UserRolesVm userRolesVm)
    {
        await Parallel.ForEachAsync(userRolesVm.AllRoles,
            async (role, cs) => { await UpdateRoleMembership(role, userRolesVm.UserId); });
    }

    private Task UpdateRoleMembership(RoleForSelectionVm? role, Guid userId)
    {
        if (role is null)
            return Task.CompletedTask;
        return role.IsSelected
            ? userRepository.AddToRoleAsync(userId, role.Id)
            : userRepository.RemoveFromRoleAsync(userId, role.Id);
    }
}