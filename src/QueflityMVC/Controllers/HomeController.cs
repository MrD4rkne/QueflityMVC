﻿using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Web.Common;
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
        var contactVmResult = await _purchasableEntityService.GetContactVmAsync(id, User.GetLoggedInUserId());
        if (contactVmResult.IsSuccess)
        {
            return View(contactVmResult.Value);
        }

        return contactVmResult.Error.Code switch
        {
            ErrorCodes.User.EMAIL_NOT_VERIFIED => RedirectToPage("RegisterConfirmation", new { email = User.FindFirstValue(ClaimTypes.Email)}),
            ErrorCodes.Purchasable.DOES_NOT_EXIST => RedirectToAction("PurchasableNotFound", "Home")
        };
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
    
    public IActionResult PurchasableNotFound()
    {
        return View();
    }
}