using Microsoft.AspNetCore.Identity;

namespace QueflityMVC.Web.Common;

public class MySignInResult : SignInResult
{
    private static readonly MySignInResult _disabled = new() { IsDisabled = true };

    private static readonly MySignInResult _requiresEmailConfirmation = new() { DoesRequireEmailConfirmation = true };

    public bool IsDisabled { get; protected set; }

    public bool DoesRequireEmailConfirmation { get; protected set; }

    public static SignInResult Disabled => _disabled;

    public static SignInResult RequiresEmailConfirmation => _requiresEmailConfirmation;
}