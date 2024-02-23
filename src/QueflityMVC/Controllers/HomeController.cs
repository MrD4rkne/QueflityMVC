using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class HomeController : Controller
{
    private readonly IPurchasableEntityService _purchasableEntityService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger, IPurchasableEntityService purchasableEntityService)
    {
        _logger = logger;
        _purchasableEntityService = purchasableEntityService;
    }

    public IActionResult Index()
    {
        return View();
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