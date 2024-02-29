using System.Diagnostics;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Purchasable;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IPurchasableEntityService _purchasableEntityService;

    public DashboardController(IPurchasableEntityService purchasableEntityService)
    {
        _purchasableEntityService = purchasableEntityService;
    }


    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_ORDER)]
    public async Task<IActionResult> Index()
    {
        var orderEditVM = await _purchasableEntityService.GetEnitiesOrderVM();
        return View(orderEditVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_ORDER)]
    public async Task<IActionResult> Index(EditOrderVM editOrderVM)
    {
        if (editOrderVM is null || editOrderVM.PurchasablesVMs is null)
        {
            return BadRequest();
        }
        UpdateOrderResult result = await _purchasableEntityService.UpdateOrderAsync(editOrderVM);
        switch (result.Status)
        {
            case UpdateOrderStatus.Success:
                return RedirectToAction(nameof(Index), "Home");
            case UpdateOrderStatus.NotValidOrder:
                ModelState.AddModelError(string.Empty, "Order is not valid");
                return View(editOrderVM);
            case UpdateOrderStatus.MissingPurchasable:
                return RedirectToAction("UpdateFailed", new UpdateOrderFailedVM() { Message="Purchasable list was altered. Please try again."});
            case UpdateOrderStatus.Exception:
                throw result.Exception!;
            default:
                throw new NotImplementedException();
        }
    }
}