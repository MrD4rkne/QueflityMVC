﻿using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

internal static class OAuthSetup
{
    internal static void AddAuthenticationWithOAuths(this WebApplicationBuilder applicationBuilder)
    {
        var services = applicationBuilder.Services;
        services
            .Configure<GoogleOAuthOptions>(
                applicationBuilder.Configuration.GetSection(GoogleOAuthOptions.SECTION_NAME));
        services.AddTransient<IConfigureOptions<GoogleOptions>, ConfigureGoogleOAuth>();

        services.AddAuthentication()
            .AddGoogle();
    }
}