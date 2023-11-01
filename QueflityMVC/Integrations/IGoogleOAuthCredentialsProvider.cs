namespace QueflityMVC.Web.Integrations
{
    public interface IGoogleOAuthCredentialsProvider
    {
        string? GetGoogleOAuthClientId();

        string? GetGoogleOAuthClientSecret();

        Tuple<string?, string?> GetGoogleOAuthCredentials();
    }
}
