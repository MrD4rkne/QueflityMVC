using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

internal class ConfigureGoogleOAuth : IConfigureNamedOptions<GoogleOptions>
{
    private readonly IOptions<GoogleOAuthOptions> _oAuthGoogleOptions;

    public ConfigureGoogleOAuth(IOptions<GoogleOAuthOptions> oAuthGoogleOptions)
    {
        _oAuthGoogleOptions = oAuthGoogleOptions;
    }

    public void Configure(GoogleOptions options)
    {
        options.ClientId = _oAuthGoogleOptions.Value.ClientId;
        options.ClientSecret = _oAuthGoogleOptions.Value.ClientSecret;
    }

    public void Configure(string? name, GoogleOptions options)
    {
        Configure(options);
    }
}