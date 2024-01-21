namespace QueflityMVC.Web.Setup.Secrets
{
    public interface IVariablesProvider
    {
        string? GetGoogleOAuthClientId();

        string? GetGoogleOAuthClientSecret();

        Tuple<string?, string?> GetGoogleOAuthCredentials();

        string? GetConnectionString();
    }
}