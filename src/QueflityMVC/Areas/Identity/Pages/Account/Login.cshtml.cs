// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

#nullable disable

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Identity.Pages.Account;

public class LoginModel : PageModel
{
    private readonly ILogger<LoginModel> _logger;
    private readonly MySignInManager _signInManager;

    public LoginModel(MySignInManager signInManager, ILogger<LoginModel> logger)
    {
        _signInManager = signInManager;
        _logger = logger;
    }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [BindProperty]
    public InputModel Input { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public IList<AuthenticationScheme> ExternalLogins { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public string ReturnUrl { get; set; }

    /// <summary>
    ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    [TempData]
    public string ErrorMessage { get; set; }

    public async Task OnGetAsync(string returnUrl = null)
    {
        if (!string.IsNullOrEmpty(ErrorMessage))
        {
            ModelState.AddModelError(string.Empty, ErrorMessage);
        }

        returnUrl ??= Url.Content("~/");

        // Clear the existing external cookie to ensure a clean login process
        await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        ReturnUrl = returnUrl;
    }

    public async Task<IActionResult> OnPostAsync(string returnUrl = null)
    {
        returnUrl ??= Url.Content("~/");

        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

        if (ModelState.IsValid)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, Input.RememberMe, false);
            if (result.Succeeded)
            {
                _logger.LogInformation("User logged in.");
                return LocalRedirect(returnUrl);
            }

            if (result.RequiresTwoFactor)
            {
                return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, Input.RememberMe });
            }

            if (result is MySignInResult { IsDisabled: true })
            {
                TempData["PopupVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Account disabled",
                    Message = "Your account has been disabled. Please contact administrator for further details.",
                    Type = PopUpType.Error
                });
                return Page();
            }

            if (result is MySignInResult { DoesRequireEmailConfirmation: true })
            {
                TempData["PopupVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Email confirmation required",
                    Message = "Please confirm your email before logging in.",
                    Type = PopUpType.Info
                });
                return Page();
            }

            if (result.IsLockedOut)
            {
                TempData["PopupVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Account locked out",
                    Message = "Your account has been locked out. Please try again later.",
                    Type = PopUpType.Error
                });
                _logger.LogWarning("User account locked out.");
                return RedirectToPage("./Lockout");
            }

            if (result.IsNotAllowed)
            {
                TempData["PopupVm"] = JsonConvert.SerializeObject(new PopUpViewModel
                {
                    Title = "Account not allowed",
                    Message = "Your account is not allowed to login. Please contact administrator for further details.",
                    Type = PopUpType.Error
                });
                return Page();
            }

            TempData["PopupVm"] = JsonConvert.SerializeObject(new PopUpViewModel
            {
                Title = "Login failed",
                Message = "Invalid login attempt. Please try again.",
                Type = PopUpType.Error
            });
            return Page();
        }

        // If we got this far, something failed, redisplay form
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

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}