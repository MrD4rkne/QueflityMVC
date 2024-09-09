using Microsoft.AspNetCore.Mvc;

namespace QueflityMVC.Web.Common;

public class HttpErrorCodesMiddleware(
    RequestDelegate next,
    ILogger<HttpErrorCodesMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        await next(context);

        if (context.Response.StatusCode >= 400)
        {
            logger.LogWarning("Error {context.Response.StatusCode} occurred. Path: {context.Request.Path}",
                context.Response.StatusCode, context.Request.Path);

            // Ensure the response has not started, then redirect
            if (!context.Response.HasStarted)
            {
                var urlHelper = context.RequestServices.GetRequiredService<IUrlHelper>();
                string redirectUrl =
                    urlHelper.Action("Error", "Home", new { area = "", id = context.Response.StatusCode });
                context.Response.Redirect(redirectUrl);
            }
        }
    }
}