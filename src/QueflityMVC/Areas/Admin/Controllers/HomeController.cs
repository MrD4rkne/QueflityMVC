using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = Policies.SEE_ADMIN_PANEL)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}