using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.User;

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
        [Authorize(Policy = "")]
        public IActionResult DeleteUser(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);

            _userService.DeleteUser(userId);

            return RedirectToAction("Index");
        }
    }
}
