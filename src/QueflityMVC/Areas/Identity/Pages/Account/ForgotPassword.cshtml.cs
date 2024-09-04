// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Newtonsoft.Json;
using QueflityMVC.Application.Emails;
using QueflityMVC.Domain.Models;
using IEmailService = QueflityMVC.Application.Emails.IEmailService;

namespace QueflityMVC.Web.Areas.Identity.Pages.Account
{
    public class ForgotPasswordModel(UserManager<ApplicationUser> userManager, IEmailService emailSender)
        : PageModel
    {
        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(Input.Email);
                if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return RedirectToPage("./ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please
                // visit https://go.microsoft.com/fwlink/?LinkID=532713
                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                var callbackUrl = Url.Page(
                    "/Account/ResetPassword",
                    pageHandler: null,
                    values: new { area = "Identity", code },
                    protocol: Request.Scheme);

                ResetPasswordEmail resetPasswordEmail = new ResetPasswordEmail
                {
                    Email = Input.Email,
                    User = user,
                    Url = HtmlEncoder.Default.Encode(callbackUrl)
                };

                var result = await emailSender.SendResetPasswordEmail(resetPasswordEmail);
                if (result.IsFailure)
                {
                    PopUpViewModel popUpViewModel = new PopUpViewModel
                    {
                        Title = "Error",
                        Message = "An error occurred while sending the email. Please try again.",
                        Type = PopUpType.Error
                    };
                    TempData["PopUpVm"] = JsonConvert.SerializeObject(popUpViewModel);
                    return Page();
                }

                return RedirectToPage("./ForgotPasswordConfirmation");
            }

            return Page();
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