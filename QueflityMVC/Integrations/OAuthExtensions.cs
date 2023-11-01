using Microsoft.AspNetCore.Authentication.Google;

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
                throw new NotImplementedException("Missing credentials!");
                // TODO: ERROR

            }
        }
    }
}