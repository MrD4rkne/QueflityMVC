using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Web.Exceptions;

namespace QueflityMVC.Web.Controllers;

public class DashboardController : Controller
{
    private readonly IProductEntityService _purchasableEntityService;

    public DashboardController(IProductEntityService purchasableEntityService)
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
        if (editOrderVm?.ProductsVMs is null) return BadRequest();
        var result = await _purchasableEntityService.UpdateOrderAsync(editOrderVm);
        if (result.IsSuccess) return RedirectToAction(nameof(Index), "Home");
        switch (result.Error.Code)
        {
            case ErrorCodes.Product.INVALID_ORDER:
                ModelState.AddModelError(string.Empty, "Order is not valid");
                return View(editOrderVm);

            case ErrorCodes.Product.PURCHASABLE_MISSING_IN_ORDER:
                return RedirectToAction("UpdateFailed",
                    new UpdateOrderFailedVm { Message = "Product list was altered. Please try again." });
            default:
                throw new UnexpectedApplicationException();
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_ORDER)]
    public IActionResult UpdateFailed(UpdateOrderFailedVm updateFailedVm)
    {
        return View(updateFailedVm);
    }
}