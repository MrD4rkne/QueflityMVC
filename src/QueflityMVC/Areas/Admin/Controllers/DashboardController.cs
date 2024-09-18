using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = Policies.ENTITIES_ORDER)]
public class DashboardController(IProductService productService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var orderEditVm = await productService.GetEntitiesOrderVm();
        return View(orderEditVm);
    }

    [HttpPost]
    public async Task<IActionResult> Index(EditOrderVm editOrderVm)
    {
        if (editOrderVm?.ProductsVMs is null)
        {
            return BadRequest();
        }

        var result = await productService.UpdateOrderAsync(editOrderVm);
        if (result.IsSuccess)
        {
            return RedirectToAction(nameof(Index), "Home");
        }

        switch (result.Error.Code)
        {
            case ErrorCodes.Product.INVALID_ORDER:
                ModelState.AddModelError(string.Empty, "Order is not valid");
                return View(editOrderVm);

            case ErrorCodes.Product.product_MISSING_IN_ORDER:
                return RedirectToAction("UpdateFailed",
                    new UpdateOrderFailedVm { Message = "Product list was altered. Please try again." });
            default:
                return this.RedirectToError();
        }
    }

    [HttpGet]
    public IActionResult UpdateFailed(UpdateOrderFailedVm updateFailedVm)
    {
        return View(updateFailedVm);
    }
}