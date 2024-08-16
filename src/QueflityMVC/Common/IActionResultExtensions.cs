using Microsoft.AspNetCore.Mvc;

namespace QueflityMVC.Web.Common;

internal static class ActionResultExtensions
{
    internal static IActionResult Error(this Controller controller)
    {
        return controller.View("Error");
    }
}