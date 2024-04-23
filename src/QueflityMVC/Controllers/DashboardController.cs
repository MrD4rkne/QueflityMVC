using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Purchasable;

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
        var orderEditVm = await _purchasableEntityService.GetEntitiesOrderVm();
        return View(orderEditVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_ORDER)]
    public async Task<IActionResult> Index(EditOrderVm editOrderVm)
    {
        if (editOrderVm is null || editOrderVm.PurchasablesVMs is null) return BadRequest();
        var result = await _purchasableEntityService.UpdateOrderAsync(editOrderVm);
        switch (result.Status)
        {
            case UpdateOrderStatus.Success:
                return RedirectToAction(nameof(Index), "Home");

            case UpdateOrderStatus.NotValidOrder:
                ModelState.AddModelError(string.Empty, "Order is not valid");
                return View(editOrderVm);

            case UpdateOrderStatus.MissingPurchasable:
                return RedirectToAction("UpdateFailed",
                    new UpdateOrderFailedVm { Message = "Purchasable list was altered. Please try again." });

            case UpdateOrderStatus.Exception:
                throw result.Exception!;
            default:
                throw new NotImplementedException();
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_ORDER)]
    public IActionResult UpdateFailed(UpdateOrderFailedVm updateFailedVm)
    {
        return View(updateFailedVm);
    }
}