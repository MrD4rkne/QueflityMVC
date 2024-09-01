// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Web.Areas.Identity.Pages.Account
{
    public class ConfirmEmailChangeModel(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        ILogger<ConfirmEmailChangeModel> logger)
        : PageModel
    {
        private readonly ILogger<ConfirmEmailChangeModel> _logger = logger;

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string PopUpVm { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string email, string code)
        {
            if (userId == null || email == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogError("Unable to load user with ID '{userId}'.", userId);
                return PageError();
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userManager.ChangeEmailAsync(user, email, code);
            if (!result.Succeeded)
            {
                logger.LogError("Error changing email for user with ID '{userId}': {errors}", userId, result.Errors);
                return PageError();
            }

            // In our UI email and user name are one and the same, so when we update the email
            // we need to update the user name.
            var setUserNameResult = await userManager.SetUserNameAsync(user, email);
            if (!setUserNameResult.Succeeded)
            {
                logger.LogError("Error setting user name for user with ID '{userId}': {errors}", userId,
                    setUserNameResult.Errors);
                return PageError();
            }

            await signInManager.RefreshSignInAsync(user);

            SetPopUpVm(new PopUpViewModel
            {
                Title = "Email changed",
                Message = "Your email has been changed.",
                Type = PopUpType.Success
            });
            return Page();
        }

        private IActionResult PageError()
        {
            SetPopUpVm(GetErrorViewModel());
            return Page();
        }

        private void SetPopUpVm(PopUpViewModel popUpViewModel)
        {
            PopUpVm = JsonConvert.SerializeObject(popUpViewModel);
        }

        private static PopUpViewModel GetErrorViewModel()
        {
            return new PopUpViewModel
            {
                Title = "Error",
                Message = "Error changing email. Please try again.",
                Type = PopUpType.Error
            };
        }
    }
}