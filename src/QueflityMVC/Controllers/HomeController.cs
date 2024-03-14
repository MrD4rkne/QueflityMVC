using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPurchasableEntityService _purchasableEntityService;

    public HomeController(ILogger<HomeController> logger, IPurchasableEntityService purchasableEntityService)
    {
        _logger = logger;
        _purchasableEntityService = purchasableEntityService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboardVm = await _purchasableEntityService.GetDashboardVmAsync();
        return View(dashboardVm);
    }

    [HttpGet]
    [Route("Details")]
    public Task<IActionResult> Details(int id)
    {
        throw new NotImplementedException();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}