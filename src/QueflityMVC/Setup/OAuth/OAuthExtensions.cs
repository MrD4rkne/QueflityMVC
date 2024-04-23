using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using QueflityMVC.Web.Setup.Secrets;
using Serilog;

namespace QueflityMVC.Web.Setup.OAuth;

public static class OAuthExtensions
{
    private const string OAUTH_GOOGLE = "Google OAuth 2.0";

    public static AuthenticationBuilder AddOAuths(this AuthenticationBuilder authenticationBuilder,
        IVariablesProvider variablesProvider)
    {
        authenticationBuilder.SetupGoogleOAuth(variablesProvider);

        return authenticationBuilder;
    }

    private static void SetupGoogleOAuth(this AuthenticationBuilder authenticationBuilder,
        IVariablesProvider credentialsProvider)
    {
        var (clientId, clientSecret) = credentialsProvider.GetGoogleOAuthCredentials();
        if (!AreGoogleSecretsValid(clientId, clientSecret))
        {
            Log.Error("Could not configure: {service}. Please check credentials.", OAUTH_GOOGLE);
            return;
        }

        authenticationBuilder.AddGoogle(opt => opt.ConfigureGoogleOptions(clientId!, clientSecret!));
        Log.Information("Configured service {service} successfully", OAUTH_GOOGLE);
    }

    private static void ConfigureGoogleOptions(this GoogleOptions options, string clientId, string clientSecret)
    {
        options.ClientId = clientId;
        options.ClientSecret = clientSecret;
    }

    private static bool AreGoogleSecretsValid(string? clientId, string? clientSecret)
    {
        return !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret);
    }
}