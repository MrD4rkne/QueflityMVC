namespace QueflityMVC.Web.Setup.Secrets
{
    public class EnviromentCredentialsProvider : IVariablesProvider
    {
        private const string OAUTH_GOOGLE_CLIENT_ID = "OAUTH_GOOGLE_CLIENT_ID";
        private const string OAUTH_GOOGLE_CLIENT_SECRET = "OAUTH_GOOGLE_CLIENT_SECRET";
        private const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";

        public EnviromentCredentialsProvider()
        { }

        public string? GetConnectionString()
        {
            return Environment.GetEnvironmentVariable(DB_CONNECTION_STRING);
        }

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