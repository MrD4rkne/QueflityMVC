﻿using System.Security.Claims;

namespace QueflityMVC.Web.Setup.Identity;

internal static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        var userId = principal?.FindFirstValue(ClaimTypes.NameIdentifier);

        return Guid.TryParse(userId, out var parsedUserId)
            ? parsedUserId
            : throw new ApplicationException("User id is unavailable");
    }
}