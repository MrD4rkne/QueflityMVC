using Microsoft.AspNetCore.Authentication;

namespace QueflityMVC.Web.Integrations
{
    public static class OAuthExtensions
    {
        private static string GOOGLE_AUTH_SECTION => "Authentication:Google";

        public static void AddExternalOAuths(this AuthenticationBuilder authenticationBuilder, ConfigurationManager configurationManager)
        {
            authenticationBuilder.AddGoogle(googleOptions =>
            {
                IConfigurationSection googleOAuthSection = configurationManager.GetSection(GOOGLE_AUTH_SECTION);
                if (googleOAuthSection is not null)
                {
                    string? clientId = googleOAuthSection["ClientId"];
                    string? clientSecret = googleOAuthSection["ClientSecret"];

                    if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
                    {
                        googleOptions.ClientId = clientId;
                        googleOptions.ClientSecret = clientSecret;
                    }
                    else
                    {
                    }
                }
                else
                {
                    // TODO: Add logging
                }
            });
        }
    }
}
