using QueflityMVC.Web.Integrations;

namespace QueflityMVC.Web.Common
{
    public class EnviromentCredentialsProvider : IGoogleOAuthCredentialsProvider
    {
        private const string OAUTH_GOOGLE_CLIENT_ID = "OAUTH_GOOGLE_CLIENT_ID";
        private const string OAUTH_GOOGLE_CLIENT_SECRET = "OAUTH_GOOGLE_CLIENT_SECRET";

        public EnviromentCredentialsProvider() { }

        public string? GetGoogleOAuthClientId()
        {
            return Environment.GetEnvironmentVariable(OAUTH_GOOGLE_CLIENT_ID);
        }

        public string? GetGoogleOAuthClientSecret()
        {
            return Environment.GetEnvironmentVariable(OAUTH_GOOGLE_CLIENT_SECRET);
        }

        public Tuple<string?, string?> GetGoogleOAuthCredentials()
        {
            return new(GetGoogleOAuthClientId(), GetGoogleOAuthClientSecret());
        }
    }
}
