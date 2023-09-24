using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;

namespace QueflityMVC.Web.Integrations
{
    public static class OAuthProviders
    {
        private static string GOOGLE_AUTH_SECTION => "Authentication:Google";

        public static void AddExternalOAuths(this AuthenticationBuilder authenticationBuilder, ConfigurationManager configurationManager)
        {
            authenticationBuilder.AddGoogle(googleOptions =>
                googleOptions.Setup(configurationManager.GetSection(GOOGLE_AUTH_SECTION)));
        }

        public static void Setup(this GoogleOptions googleOptions, IConfigurationSection googleOAuthSection)
        {
            if (googleOAuthSection is null)
            {
                // TODO: ERROR
                return;
            }

            string? clientId = googleOAuthSection["ClientId"];
            string? clientSecret = googleOAuthSection["ClientSecret"];

            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                googleOptions.ClientId = clientId;
                googleOptions.ClientSecret = clientSecret;
            }
            else
            {
                // TODO: ERROR
            }
        }
    }
}
