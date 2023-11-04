using Microsoft.AspNetCore.Authentication.Google;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Integrations
{
    public static class OAuthExtensions
    {
        public static void Setup(this GoogleOptions googleOptions, IGoogleOAuthCredentialsProvider credentialsProvider)
        {
            googleOptions.Setup(credentialsProvider.GetGoogleOAuthCredentials());
        }

        public static void Setup(this GoogleOptions googleOptions, Tuple<string?, string?> credentials)
        {
            ArgumentNullException.ThrowIfNull(credentials, nameof(credentials));

            string? clientId = credentials.Item1;
            string? clientSecret = credentials.Item2;

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                googleOptions.ClientId = clientId;
                googleOptions.ClientSecret = clientSecret;
            }
            else
            {
                throw new ConfigurationException("Could not found Google OAuth credentials.");
            }
        }
    }
}