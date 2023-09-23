using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Controllers
{
    [Route("Admin")]
    public class AdminController : Controller
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService) {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Policy = "CanListUsers")]
        public async Task<IActionResult> Index()
        {
            ListUsersVM listUserVM = new()
            {
                Pagination = PaginationFactory.Default<UserForListVM>()
            };
            return await Index(listUserVM);
        }

        [HttpPost]
        [Authorize(Policy = "CanListUsers")]
        public async Task<IActionResult> Index(ListUsersVM listUsersVM)
        {
            if (listUsersVM is null)
            {
                return BadRequest();
            }
            listUsersVM.UserNameFilter ??= string.Empty;

            ListUsersVM listVM = await _userService.GetFilteredList(listUsersVM);
            return View(listVM);
        }

        [HttpGet]
        [Route("DisableUser")]
        [Authorize(Policy = "CanDisableUser")]
        public async Task<IActionResult> DisableUser(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);
            string? callerId = User.GetLoggedInUserId();
            if (IsTheSameUser(callerId, userId))
            {
                return Unauthorized("User cannot disable himself.");
            }

            await _userService.DisableUser(userId);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("EnableUser")]
        [Authorize(Policy = "CanEnableUser")]
        public async Task<IActionResult> EnableUser(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);
            await _userService.EnableUser(userId);

            return RedirectToAction("Index");
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
}
