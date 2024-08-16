using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Web.Controllers;

[Route("User")]
public class UserController(IUserService userService, IUserContext userContext) : Controller
{
    [HttpGet]
    [Authorize(Policy = Policies.USERS_LIST)]
    public async Task<IActionResult> Index()
    {
        ListUsersVm listUserVm = new()
        {
            Pagination = PaginationFactory.Default<UserForListVm>()
        };
        return await Index(listUserVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.USERS_LIST)]
    public async Task<IActionResult> Index(ListUsersVm listUsersVm)
    {
        if (listUsersVm is null) return BadRequest();
        listUsersVm.UserNameFilter ??= string.Empty;

        var listVm = await userService.GetFilteredListAsync(listUsersVm);
        return View(listVm);
    }

    [HttpGet]
    [Route("DisableUser")]
    [Authorize(Policy = Policies.USER_DISABLE)]
    public async Task<IActionResult> DisableUser(Guid userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var result = await userService.DisableUserAsync(userId);
        if (result.IsSuccess) return RedirectToAction("Index");

        return result.Error.Code switch
        {
            ErrorCodes.User.CANNOT_MANAGE_SELF => Forbid(),
            ErrorCodes.User.DOES_NOT_EXIST => NotFound()
        };
    }

    [HttpGet]
    [Route("EnableUser")]
    [Authorize(Policy = Policies.USER_ENABLE)]
    public async Task<IActionResult> EnableUser(Guid userId)
    {
        ArgumentNullException.ThrowIfNull(userId);

        var result = await userService.EnableUserAsync(userId);
        if (result.IsSuccess) return RedirectToAction("Index");

        return result.Error.Code switch
        {
            ErrorCodes.User.CANNOT_MANAGE_SELF => Forbid(),
            ErrorCodes.User.DOES_NOT_EXIST => NotFound()
        };
    }

    [HttpGet]
    [Route("ManageUserRoles")]
    [Authorize(Policy = Policies.USER_ROLES_VIEW)]
    public async Task<IActionResult> ManageUserRoles(Guid userId)
    {
        var userRolesVm = await userService.GetUsersRolesVmAsync(userId);
        userRolesVm.CanCallerManage = CanUserManageRoles(userId);
        return View(userRolesVm);
    }

    [HttpPost]
    [Route("ManageUserRoles")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.USER_ROLES_MANAGE)]
    public async Task<IActionResult> ManageUserRoles(UserRolesVm userRolesVm)
    {
        ArgumentNullException.ThrowIfNull(userRolesVm);
        if (!CanUserManageRoles(userRolesVm.UserId)) return Forbid();

        await userService.UpdateUserRolesAsync(userRolesVm);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("ManageUserClaims")]
    [Authorize(Policy = Policies.USER_CLAIMS_VIEW)]
    public async Task<IActionResult> ManageUserClaims(Guid userId)
    {
        var userClaimsVm = await userService.GetUsersClaimsVmAsync(userId);
        userClaimsVm.CanCallerManage = CanUserManageClaims(userId);

        return View(userClaimsVm);
    }

    [HttpPost]
    [Route("ManageUserClaims")]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.USER_CLAIMS_MANAGE)]
    public async Task<IActionResult> ManageUserClaims(UserClaimsVm userClaimsVm)
    {
        ArgumentNullException.ThrowIfNull(userClaimsVm);
        if (!CanUserManageClaims(userClaimsVm.UserId)) return Forbid();

        await userService.UpdateUserClaimsAsync(userClaimsVm);
        return RedirectToAction("Index");
    }

    private bool CanUserManageRoles(Guid userToBeManagedId)
    {
        return !IsTheSameUser(userContext.UserId, userToBeManagedId) &&
               User.HasClaim(Claims.USER_ROLES_MANAGE, Claims.USER_ROLES_MANAGE);
    }

    private bool CanUserManageClaims(Guid userToBeManagedId)
    {
        return !IsTheSameUser(userContext.UserId, userToBeManagedId) &&
               User.HasClaim(Claims.USER_CLAIMS_MANAGE, Claims.USER_CLAIMS_MANAGE);
    }

    private static bool IsTheSameUser(Guid? userIdA, Guid? userIdB)
    {
        return userIdA == userIdB;
    }
}