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
    public class ConfirmEmailModel(UserManager<ApplicationUser> userManager, ILogger<ConfirmEmailChangeModel> logger)
        : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string PopupVm { get; set; }

        public async Task<IActionResult> OnGetAsync(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                logger.LogError("Unable to load user with ID '{userId}'.", userId);
            }

            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
            {
                logger.LogError("Error confirming email for user with ID '{userId}': {errors}", userId, result.Errors);
                PopupVm = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Error confirming your email",
                    Message = "Please try again.",
                    Type = PopUpType.Error
                });
            }
            else
            {
                PopupVm = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Email confirmed",
                    Message = "Thank you for confirming your email.",
                    Type = PopUpType.Success
                });
            }

            return Page();
        }
    }
}