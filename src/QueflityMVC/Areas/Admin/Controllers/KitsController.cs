using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Route("Admin/Kits")]
public class KitsController(
    IKitService kitService,
    IValidator<KitVm> kitValidator,
    IValidator<ElementVm> elemValidator,
    ILogger<KitsController> logger)
    : Controller
{
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(int? itemId)
    {
        ListKitsVm listKitsVm = new()
        {
            ItemId = itemId,
            Pagination = PaginationFactory.Default<KitForListVm>()
        };
        return await Index(listKitsVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListKitsVm listKitsVm)
    {
        if (listKitsVm is null)
        {
            return BadRequest();
        }

        listKitsVm.NameFilter ??= string.Empty;

        var listVm = await kitService.GetFilteredListAsync(listKitsVm);
        return View(listVm);
    }

    [Route("Create")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public IActionResult Create()
    {
        return View();
    }

    [Route("Create")]
    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(KitVm createKitVm, bool shouldRouteToDetails = false)
    {
        var validationResults = await kitValidator.ValidateAsync(createKitVm);

        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);

            return View("Create", createKitVm);
        }

        var result = await kitService.CreateKitAsync(createKitVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return shouldRouteToDetails
                    ? RedirectToAction("Details", new { id = result.Value.Id })
                    : RedirectToAction("Index");
            default:
                logger.LogError("Kit creation failed: {Kit}: {error}", createKitVm, result.Error);
                return this.RedirectToError();
        }
    }

    [Route("Details")]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Details(int id)
    {
        var kitDetailsResult = await kitService.GetDetailsVmAsync(id);
        if (kitDetailsResult.IsSuccess)
        {
            return View(kitDetailsResult.Value);
        }

        return kitDetailsResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var kitToEditResult = await kitService.GetKitVmForEditAsync(id);
        if (kitToEditResult.IsSuccess)
        {
            return View(kitToEditResult.Value);
        }

        return kitToEditResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(KitVm editedKitVm)
    {
        if (editedKitVm is null)
        {
            return BadRequest();
        }

        editedKitVm.ElementCount = await kitService.GetElementCount(editedKitVm.Id);
        var validationResults = await kitValidator.ValidateAsync(editedKitVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("Edit", editedKitVm);
        }

        var result = await kitService.EditKitAsync(editedKitVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Details", new { id = result.Value.Id });
            case { IsFailure: true, Error.Code: ErrorCodes.Kits.DOES_NOT_EXIST }:
                return NotFound();
            default:
                logger.LogError("Kit update failed: {Kit}: {error}", editedKitVm, result.Error);
                return this.RedirectToError();
        }
    }

    [Route("Delete")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id, int? itemId)
    {
        var results = await kitService.DeleteKitAsync(id);
        if (results.IsSuccess)
        {
            return RedirectToAction("Index", new { itemId });
        }

        return results.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [Route("ListItemsForComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(int kitId)
    {
        var getFilteredComponentsResult = await kitService.GetFilteredListForComponentsAsync(kitId);
        if (getFilteredComponentsResult.IsSuccess)
        {
            return View(getFilteredComponentsResult.Value);
        }

        return getFilteredComponentsResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [Route("ListItemsForComponent")]
    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(ListItemsForComponentsVm listItemsForComponentsVm)
    {
        var filterComponentsResult = await kitService.GetFilteredListForComponentsAsync(listItemsForComponentsVm);
        if (filterComponentsResult.IsSuccess)
        {
            return View(filterComponentsResult.Value);
        }

        return filterComponentsResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [Route("AddComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> AddComponent(int kitId, int itemId)
    {
        var addingComponentVm = await kitService.GetVmForAddingElementAsync(kitId, itemId);
        return View(addingComponentVm);
    }

    [Route("AddComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> AddComponent(ElementVm elementVm)
    {
        var validationResults = await elemValidator.ValidateAsync(elementVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("AddComponent", elementVm);
        }

        await kitService.AddElementAsync(elementVm);
        return RedirectToAction("Details", new { id = elementVm.KitDetailsVm.Id });
    }

    [Route("EditComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(int kitId, int itemId)
    {
        var addingComponentVm = await kitService.GetVmForEditingElementAsync(kitId, itemId);
        return View(addingComponentVm);
    }

    [Route("EditComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(ElementVm elementVm)
    {
        var validationResults = await elemValidator.ValidateAsync(elementVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("AddComponent", elementVm);
        }

        await kitService.EditElementAsync(elementVm);
        return RedirectToAction("Details", new { id = elementVm.KitDetailsVm.Id });
    }

    [Route("DeleteComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> DeleteComponent(int kitId, int itemId)
    {
        await kitService.DeleteElementAsync(kitId, itemId);
        return RedirectToAction("Details", new { id = kitId });
    }
}