using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Setup.Secrets
{
    public class AppSecretsCredentialProvider : IVariablesProvider
    {
        private const string OAUTH_GOOGLE_CLIENT_ID = "ClientId";
        private const string OAUTH_GOOGLE_CLIENT_SECRET = "ClientSecret";
        private const string OAUTH_GOOGLE_SECTION = "Authentication:Google";
        private const string DB_CONNECTION_STRING = "Database:ConnectionString";

        private ConfigurationManager _configManager;

        public AppSecretsCredentialProvider(ConfigurationManager configManager)
        {
            _configManager = configManager;
        }

        public string? GetGoogleOAuthClientId()
        {
            var googleSection = GetGoogleSection();
            return googleSection[OAUTH_GOOGLE_CLIENT_ID];
        }

        public string? GetGoogleOAuthClientSecret()
        {
            var googleSection = GetGoogleSection();
            return googleSection[OAUTH_GOOGLE_CLIENT_SECRET];
        }

        private IConfigurationSection GetGoogleSection()
        {
            var credentialsSection = _configManager.GetSection(OAUTH_GOOGLE_SECTION);
            if (string.IsNullOrEmpty(credentialsSection[OAUTH_GOOGLE_CLIENT_ID]))
                throw new ConfigurationException("Could not found Google OAuth credentials: client id.");
            if (string.IsNullOrEmpty(credentialsSection[OAUTH_GOOGLE_CLIENT_SECRET]))
                throw new ConfigurationException("Could not found Google OAuth credentials: client secret.");

            return credentialsSection;
        }

        public Tuple<string?, string?> GetGoogleOAuthCredentials()
        {
            return new(GetGoogleOAuthClientId(), GetGoogleOAuthClientSecret());
        }

        public string? GetConnectionString()
        {
            string? connectionString = _configManager.GetValue<string>(DB_CONNECTION_STRING);
            if (string.IsNullOrEmpty(connectionString))
                throw new ConfigurationException("Database connection string is null or empty.");
            return connectionString;
        }
    }
}