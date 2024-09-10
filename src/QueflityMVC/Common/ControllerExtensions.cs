using Microsoft.AspNetCore.Mvc;

namespace QueflityMVC.Web.Common;

internal static class ControllerExtensions
{
    internal static IActionResult RedirectToError(this Controller controller)
    {
        return controller.RedirectToAction("Error", "Home", new { area = "" });
    }
}