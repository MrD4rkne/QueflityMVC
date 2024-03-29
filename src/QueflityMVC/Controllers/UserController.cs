﻿using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Controllers;

[Route("User")]
public class UserController : Controller
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    [Authorize(Policy = Policies.USERS_LIST)]
    public async Task<IActionResult> Index()
    {
        ListUsersVM listUserVM = new()
        {
            Pagination = PaginationFactory.Default<UserForListVM>()
        };
        return await Index(listUserVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.USERS_LIST)]
    public async Task<IActionResult> Index(ListUsersVM listUsersVM)
    {
        if (listUsersVM is null)
        {
            return BadRequest();
        }
        listUsersVM.UserNameFilter ??= string.Empty;

        ListUsersVM listVM = await _userService.GetFilteredListAsync(listUsersVM);
        return View(listVM);
    }

    [HttpGet]
    [Route("DisableUser")]
    [Authorize(Policy = Policies.USER_DISABLE)]
    public async Task<IActionResult> DisableUser(string userId)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        string? callerId = User.GetLoggedInUserId();
        if (IsTheSameUser(callerId, userId))
        {
            return Unauthorized("User cannot disable himself.");
        }

        await _userService.DisableUserAsync(userId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("EnableUser")]
    [Authorize(Policy = Policies.USER_ENABLE)]
    public async Task<IActionResult> EnableUser(string userId)
    {
        ArgumentException.ThrowIfNullOrEmpty(userId);
        await _userService.EnableUserAsync(userId);
        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("ManageUserRoles")]
    [Authorize(Policy = Policies.USER_ROLES_VIEW)]
    public async Task<IActionResult> ManageUserRoles(string userId)
    {
        UserRolesVM userRolesVM = await _userService.GetUsersRolesVMAsync(userId);
        userRolesVM.CanCallerManage = CanUserManageRoles(callerPrincipal: User, userToBeManagedId: userId);
        return View(userRolesVM);
    }

    [HttpPost]
    [Route("ManageUserRoles")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.USER_ROLES_MANAGE)]
    public async Task<IActionResult> ManageUserRoles(UserRolesVM userRolesVM)
    {
        ArgumentNullException.ThrowIfNull(userRolesVM);
        ArgumentNullException.ThrowIfNullOrEmpty(userRolesVM.UserId);
        if (!CanUserManageRoles(callerPrincipal: User, userToBeManagedId: userRolesVM.UserId))
        {
            return Forbid();
        }

        await _userService.UpdateUserRolesAsync(userRolesVM);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("ManageUserClaims")]
    [Authorize(Policy = Policies.USER_CLAIMS_VIEW)]
    public async Task<IActionResult> ManageUserClaims(string userId)
    {
        UserClaimsVM userClaimsVM = await _userService.GetUsersClaimsVMAsync(userId);
        userClaimsVM.CanCallerManage = CanUserManageClaims(callerPrincipal: User, userToBeManagedId: userId);

        return View(userClaimsVM);
    }

    [HttpPost]
    [Route("ManageUserClaims")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.USER_CLAIMS_MANAGE)]
    public async Task<IActionResult> ManageUserClaims(UserClaimsVM userClaimsVM)
    {
        ArgumentNullException.ThrowIfNull(userClaimsVM);
        ArgumentNullException.ThrowIfNullOrEmpty(userClaimsVM.UserId);
        if (!CanUserManageClaims(callerPrincipal: User, userToBeManagedId: userClaimsVM.UserId))
        {
            return Forbid();
        }

        await _userService.UpdateUserClaimsAsync(userClaimsVM);
        return RedirectToAction("Index");
    }

    // TODO: force user to logout after permissions update

    private static bool CanUserManageRoles(ClaimsPrincipal callerPrincipal, string userToBeManagedId)
    {
        string? callerId = callerPrincipal.GetLoggedInUserId();
        if (IsTheSameUser(callerId, userToBeManagedId))
        {
            return false;
        }
        return callerPrincipal.HasClaim(Claims.USER_ROLES_MANAGE, Claims.USER_ROLES_MANAGE);
    }

    private static bool CanUserManageClaims(ClaimsPrincipal callerPrincipal, string userToBeManagedId)
    {
        string? callerId = callerPrincipal.GetLoggedInUserId();
        if (IsTheSameUser(callerId, userToBeManagedId))
        {
            return false;
        }
        return callerPrincipal.HasClaim(Claims.USER_ROLES_MANAGE, Claims.USER_ROLES_MANAGE) && callerPrincipal.HasClaim(Claims.USER_CLAIMS_MANAGE, Claims.USER_CLAIMS_MANAGE);
    }

    private static bool IsTheSameUser(string? userIdA, string? userIdB)
    {
        if (string.IsNullOrEmpty(userIdA) || string.IsNullOrEmpty(userIdB))
        {
            return false;
        }

        return userIdA.Equals(userIdB);
    }
}