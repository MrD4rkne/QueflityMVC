// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Web.Areas.Identity.Pages.Account.Manage
{
    public class TwoFactorAuthenticationModel : PageModel
    {
        private readonly ILogger<TwoFactorAuthenticationModel> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public TwoFactorAuthenticationModel(
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            ILogger<TwoFactorAuthenticationModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool HasAuthenticator { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public int RecoveryCodesLeft { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public bool Is2faEnabled { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public bool IsMachineRemembered { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            HasAuthenticator = await _userManager.GetAuthenticatorKeyAsync(user) != null;
            Is2faEnabled = await _userManager.GetTwoFactorEnabledAsync(user);
            IsMachineRemembered = await _signInManager.IsTwoFactorClientRememberedAsync(user);
            RecoveryCodesLeft = await _userManager.CountRecoveryCodesAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await _signInManager.ForgetTwoFactorClientAsync();

            TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
            {
                Title = "Two Factor Authentication",
                Message =
                    "The current browser has been forgotten. When you login again from this browser you will be prompted for your two-factor authentication code.",
                Type = PopUpType.Success
            });
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAcceptPoliciesAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Unable to load user with ID '{userId}'.", _userManager.GetUserId(User));
                TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Privacy Policy",
                    Message = "An error occurred while accepting the Privacy Policy. Please try again.",
                    Type = PopUpType.Error
                });
                return RedirectToPage();
            }

            if (HttpContext.Features.Get<ITrackingConsentFeature>() is not ITrackingConsentFeature
                trackingConsentFeature)
            {
                _logger.LogError("Unable to load ITrackingConsentFeature.");
                TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Privacy Policy",
                    Message = "An error occurred while accepting the Privacy Policy. Please try again.",
                    Type = PopUpType.Error
                });
                return RedirectToPage();
            }

            trackingConsentFeature.GrantConsent();
            await _userManager.UpdateAsync(user);

            TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
            {
                Title = "Privacy Policy",
                Message = "You have accepted the Privacy Policy.",
                Type = PopUpType.Success
            });
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostEnable2FAAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogError("Unable to load user with ID '{userId}'.", _userManager.GetUserId(User));
                TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Two Factor Authentication",
                    Message = "An error occurred while enabling Two Factor Authentication. Please try again.",
                    Type = PopUpType.Error
                });
                return RedirectToPage();
            }

            var result = await _userManager.SetTwoFactorEnabledAsync(user, true);
            if (!result.Succeeded)
            {
                _logger.LogError("An error occurred while enabling Two Factor Authentication for {userId}: {errors}.",
                    user.Id, result.Errors);
                TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Two Factor Authentication",
                    Message = "An error occurred while enabling Two Factor Authentication.",
                    Type = PopUpType.Error
                });
                return RedirectToPage();
            }

            TempData["PopUpVm"] = JsonConvert.SerializeObject(new PopUpViewModel
            {
                Title = "Two Factor Authentication",
                Message =
                    "Two Factor Authentication has been enabled successfully. Due to this change, you have been logged out. Please login again.",
                Type = PopUpType.Success
            });
            return RedirectToPage();
        }
    }
}