using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using QueflityMVC.Web.Common;
using QueflityMVC.Web.Setup.Secrets;

namespace QueflityMVC.Web.Setup.OAuth;

public static class OAuthExtensions
{
    public static AuthenticationBuilder AddOAuths(this AuthenticationBuilder authenticationBuilder, IVariablesProvider variablesProvider)
    {
        authenticationBuilder.AddGoogle(options => options.Setup(variablesProvider));

        return authenticationBuilder;
    }

    private static void Setup(this GoogleOptions googleOptions, IVariablesProvider credentialsProvider)
    {
        var googleOAuthSecrets = credentialsProvider.GetGoogleOAuthCredentials();
        string? clientId = googleOAuthSecrets.Item1;
        string? clientSecret = googleOAuthSecrets.Item2;

        if (AreGoogleSecretsValid(clientId, clientSecret))
        {
            googleOptions.ClientId = clientId!;
            googleOptions.ClientSecret = clientSecret!;
        }
        else
        {
            throw new ConfigurationException("Could not found Google OAuth credentials.");
        }
    }

    private static bool AreGoogleSecretsValid(string? clientId, string? clientSecret)
    {
        return !string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret);
    }
}