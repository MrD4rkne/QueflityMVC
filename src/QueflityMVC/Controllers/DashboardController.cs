using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IPurchasableEntityService _purchasableEntityService;

    public DashboardController(IPurchasableEntityService purchasableEntityService)
    {
        _purchasableEntityService = purchasableEntityService;
    }

    public async Task<IActionResult> Index()
    {
        var orderEditVM = await _purchasableEntityService.GetEnitiesOrderVM();
        return View(orderEditVM);
    }
}