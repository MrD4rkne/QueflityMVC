using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

internal class ConfigureGoogleOAuth(IOptions<GoogleOAuthOptions> oAuthGoogleOptions) : IConfigureOptions<GoogleOptions>
{
    public void Configure(GoogleOptions options)
    {
        options.ClientId = oAuthGoogleOptions.Value.ClientId;
        options.ClientSecret = oAuthGoogleOptions.Value.ClientSecret;
    }
}