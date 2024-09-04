// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using QueflityMVC.Application.Emails;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ResendEmailConfirmationModel : PageModel
    {
        private readonly IEmailService _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;

        public ResendEmailConfirmationModel(UserManager<ApplicationUser> userManager, IEmailService emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.Email);
            if (user is not null)
            {
                string userId = await _userManager.GetUserIdAsync(user);
                string code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                string callbackUrl = Url.Page(
                    "/Account/ConfirmEmail",
                    null,
                    new { userId, code },
                    Request.Scheme);

                EmailConfirmation emailConfirmationEmail = new EmailConfirmation
                {
                    Email = Input.Email,
                    User = user,
                    Url = HtmlEncoder.Default.Encode(callbackUrl)
                };
                var result = await _emailSender.SendEmailConfirmationAsync(emailConfirmationEmail);
                if (result.IsFailure)
                {
                    var popUp = new PopUpViewModel
                    {
                        Title = "Error",
                        Message = "An error occurred while sending the email.",
                        Type = PopUpType.Error
                    };
                    TempData["PopupVm"] = JsonConvert.SerializeObject(popUp);

                    return RedirectToPage("Login");
                }
            }

            var popUpViewModel = new PopUpViewModel
            {
                Title = "Verification email",
                Message = "Please check your email.",
                Type = PopUpType.Info
            };
            TempData["PopupVm"] = JsonConvert.SerializeObject(popUpViewModel);

            return RedirectToPage("Login");
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }
    }
}