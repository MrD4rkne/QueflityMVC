namespace QueflityMVC.Web.Setup.Secrets;

public class EnviromentCredentialsProvider : IVariablesProvider
{
    private const string OAUTH_GOOGLE_CLIENT_ID = "OAUTH_GOOGLE_CLIENT_ID";
    private const string OAUTH_GOOGLE_CLIENT_SECRET = "OAUTH_GOOGLE_CLIENT_SECRET";
    private const string DB_CONNECTION_STRING = "DB_CONNECTION_STRING";

    public EnviromentCredentialsProvider()
    {
    }

    public string? GetConnectionString()
    {
        //return "Server=172.24.48.126;Database=QueflityDb;User Id=SA;Password=Lf5892dENz;MultipleActiveResultSets=true;TrustServerCertificate=true";
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