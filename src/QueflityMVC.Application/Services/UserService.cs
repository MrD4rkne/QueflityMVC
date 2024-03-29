﻿using System.Data;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public async Task DisableUserAsync(string userToDisableId)
    {
        bool doesUserExists = await _userRepository.DoesUserExistAsync(userToDisableId);
        if (!doesUserExists)
        {
            throw new EntityNotFoundException(entityName: nameof(ApplicationUser));
        }

        await _userRepository.DisableUserAsync(userToDisableId);
    }

    public async Task EnableUserAsync(string userToEnableId)
    {
        bool doesUserExists = await _userRepository.DoesUserExistAsync(userToEnableId);
        if (!doesUserExists)
        {
            throw new EntityNotFoundException(entityName: nameof(ApplicationUser));
        }

        await _userRepository.EnableUserAsync(userToEnableId);
    }

    public async Task<ListUsersVM> GetFilteredListAsync(ListUsersVM listUsersVM)
    {
        IQueryable<ApplicationUser> matchingUsers = _userRepository.GetFilteredUsers(listUsersVM.UserNameFilter);
        listUsersVM.Pagination = await matchingUsers.Paginate<ApplicationUser, UserForListVM>(listUsersVM.Pagination, _mapper.ConfigurationProvider);

        return listUsersVM;
    }

    public async Task<UserClaimsVM> GetUsersClaimsVMAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var allClaims = Constants.Claims.GetAll()
            .Select(str => new ClaimForSelectionVM(str));
        var assignedClaimsIds = await _userRepository.GetAssignedClaimsIdsAsync(userId);

        UserClaimsVM userClaimsVM = new()
        {
            UserId = userId,
            Username = user.UserName,
            IsEnabled = user.IsEnabled,
            AllClaims = allClaims.ToList(),
            AssignedClaimsIds = assignedClaimsIds.ToList()
        };
        return userClaimsVM;
    }

    public async Task<UserRolesVM> GetUsersRolesVMAsync(string userId)
    {
        var user = await _userRepository.GetUserByIdAsync(userId);
        if (user is null)
        {
            throw new EntityNotFoundException();
        }

        var allRoles = _userRepository.GetAllRoles()
            .ProjectTo<RoleForSelectionVM>(_mapper.ConfigurationProvider);

        var assignedRolesIds = await _userRepository.GetAssignedRolesNamesAsync(userId);

        UserRolesVM userRolesVM = new()
        {
            UserId = userId,
            Username = user.UserName,
            IsEnabled = user.IsEnabled,
            AllRoles = allRoles.ToList(),
            AssignedRolesNames = assignedRolesIds.ToList()
        };
        return userRolesVM;
    }

    public async Task UpdateUserClaimsAsync(UserClaimsVM userClaimsVM)
    {
        string[] claimsToGive = userClaimsVM.AllClaims
            .Where(x => x.IsSelected)
            .Select(x => x.Id)
            .ToArray();
        string[] claimsToRemove = userClaimsVM.AllClaims
            .Where(x => x.IsSelected == false)
            .Select(x => x.Id)
            .ToArray();

        await _userRepository.GiveClaimsAsync(userClaimsVM.UserId, claimsToGive);
        await _userRepository.RemoveClaimsAsync(userClaimsVM.UserId, claimsToRemove);
    }

    public async Task UpdateUserRolesAsync(UserRolesVM userRolesVM)
    {
        await Parallel.ForEachAsync(userRolesVM.AllRoles,
            async (role, cs) => { await UpdateRoleMembership(role, userRolesVM.UserId); });
    }

    private Task UpdateRoleMembership(RoleForSelectionVM role, string userId)
    {
        if (role is null)
            return Task.CompletedTask;

        if (role.IsSelected)
        {
            return _userRepository.AddToRoleAsync(userId, role.Id);
        }
        else
        {
            return _userRepository.RemoveFromRoleAsync(userId, role.Id);
        }
    }
}