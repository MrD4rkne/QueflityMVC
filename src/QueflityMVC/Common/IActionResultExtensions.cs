using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QueflityMVC.Application.Results;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Common;

internal static class ActionResultExtensions
{
    internal static IActionResult Error(this Controller controller)
    {
        return controller.View("Error");
    }
}