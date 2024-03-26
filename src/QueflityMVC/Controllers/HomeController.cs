using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Shared;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Other;
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
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(int id)
    {
        var contactAboutPurchasableVm = await _purchasableEntityService.GetContactVmAsync(id);
        return View(contactAboutPurchasableVm);
    }
    
    [HttpPost]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(MessageVm messageVm)
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