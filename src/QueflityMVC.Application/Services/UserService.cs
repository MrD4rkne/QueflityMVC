using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Exceptions.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task DisableUserAsync(string userToDisableId)
    {
        var doesUserExists = await _userRepository.DoesUserExistAsync(userToDisableId);
        if (!doesUserExists) throw new EntityNotFoundException(entityName: nameof(ApplicationUser));

        await _userRepository.DisableUserAsync(userToDisableId);
    }

    public async Task EnableUserAsync(string userToEnableId)
    {
        var doesUserExists = await _userRepository.DoesUserExistAsync(userToEnableId);
        if (!doesUserExists) throw new EntityNotFoundException(entityName: nameof(ApplicationUser));

        await _userRepository.EnableUserAsync(userToEnableId);
    }

    public async Task<ListUsersVm> GetFilteredListAsync(ListUsersVm listUsersVm)
    {
        var matchingUsers = _userRepository.GetFilteredUsers(listUsersVm.UserNameFilter);
        listUsersVm.Pagination = await matchingUsers.Paginate(listUsersVm.Pagination, _mapper.ConfigurationProvider);

        return listUsersVm;
    }

    public async Task<UserClaimsVm> GetUsersClaimsVmAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new EntityNotFoundException();

        var allClaims = Claims.GetAll()
            .Select(str => new ClaimForSelectionVm(str));
        var assignedClaimsIds = await _userRepository.GetAssignedClaimsIdsAsync(userId);

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

    public async Task<UserRolesVm> GetUsersRolesVmAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null) throw new EntityNotFoundException();

        var allRoles = _userRepository.GetAllRoles()
            .ProjectTo<RoleForSelectionVm>(_mapper.ConfigurationProvider);

        var assignedRolesIds = await _userRepository.GetAssignedRolesNamesAsync(userId);

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

        await _userRepository.GiveClaimsAsync(userClaimsVm.UserId, claimsToGive);
        await _userRepository.RemoveClaimsAsync(userClaimsVm.UserId, claimsToRemove);
    }

    public async Task UpdateUserRolesAsync(UserRolesVm userRolesVm)
    {
        await Parallel.ForEachAsync(userRolesVm.AllRoles,
            async (role, cs) => { await UpdateRoleMembership(role, userRolesVm.UserId); });
    }

    private Task UpdateRoleMembership(RoleForSelectionVm? role, string userId)
    {
        if (role is null)
            return Task.CompletedTask;
        return role.IsSelected
            ? _userRepository.AddToRoleAsync(userId, role.Id)
            : _userRepository.RemoveFromRoleAsync(userId, role.Id);
    }
}