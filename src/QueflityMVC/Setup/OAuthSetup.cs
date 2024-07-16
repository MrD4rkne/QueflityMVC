using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;
using QueflityMVC.Web.Setup.Identity;
using Serilog;

namespace QueflityMVC.Web.Setup;

internal static class OAuthSetup
{
    internal static void AddAuthenticationWithOAuths(this WebApplicationBuilder applicationBuilder)
    {
        IServiceCollection services = applicationBuilder.Services;
        services
            .Configure<GoogleOAuthOptions>(applicationBuilder.Configuration.GetSection(GoogleOAuthOptions.SECTION_NAME));
        services.AddTransient<IConfigureOptions<GoogleOptions>, ConfigureGoogleOAuth>();
        
        services.AddAuthentication()
            .AddGoogle((opt =>
            {
                GoogleOAuthOptions googleOAuthOptions = applicationBuilder.Services.BuildServiceProvider()
                    .GetRequiredService<IOptions<GoogleOAuthOptions>>().Value;
                opt.ClientId = googleOAuthOptions.ClientId;
                opt.ClientSecret = googleOAuthOptions.ClientSecret;
            }));
    }
}