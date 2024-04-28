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
using QueflityMVC.Web.Exceptions;

namespace QueflityMVC.Web.Controllers;

[Route("Kits")]
public class KitsController : Controller
{
    private readonly IValidator<ElementVm> _elemValidator;
    private readonly IKitService _kitService;
    private readonly IValidator<KitVm> _kitValidator;

    public KitsController(IKitService kitService, IValidator<KitVm> kitValidator, IValidator<ElementVm> elemValidator)
    {
        _kitService = kitService;
        _kitValidator = kitValidator;
        _elemValidator = elemValidator;
    }

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
        if (listKitsVm is null) return BadRequest();
        listKitsVm.NameFilter ??= string.Empty;

        var listVm = await _kitService.GetFilteredListAsync(listKitsVm);
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
        var validationResults = await _kitValidator.ValidateAsync(createKitVm);

        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);

            return View("Create", createKitVm);
        }

        var kitId = await _kitService.CreateKitAsync(createKitVm);

        if (shouldRouteToDetails)
            return RedirectToAction("Details", new { id = kitId });

        return RedirectToAction("Index");
    }

    [Route("Details")]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Details(int id)
    {
        var kitDetailsResult = await _kitService.GetDetailsVmAsync(id);
        if (kitDetailsResult.IsSuccess) return View(kitDetailsResult.Value);

        return kitDetailsResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var kitToEditResult = await _kitService.GetKitVmForEditAsync(id);
        if (kitToEditResult.IsSuccess) return View(kitToEditResult.Value);
        return kitToEditResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(KitVm editedKitVm)
    {
        var validationResults = await _kitValidator.ValidateAsync(editedKitVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("Edit", editedKitVm);
        }

        var kitId = await _kitService.EditKitAsync(editedKitVm);
        return RedirectToAction("Details", new { id = kitId });
    }

    [Route("Delete")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        var results = await _kitService.DeleteKitAsync(id);
        if (results.IsSuccess) return RedirectToAction("Index");
        return results.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("ListItemsForComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(int kitId)
    {
        var getFilteredComponentsResult = await _kitService.GetFilteredListForComponentsAsync(kitId);
        if (getFilteredComponentsResult.IsSuccess) return View(getFilteredComponentsResult.Value);

        throw new UnexpectedApplicationException();
    }

    [Route("ListItemsForComponent")]
    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(ListItemsForComponentsVm listItemsForComponentsVm)
    {
        var filterComponentsResult = await _kitService.GetFilteredListForComponentsAsync(listItemsForComponentsVm);
        if (filterComponentsResult.IsSuccess) return View(filterComponentsResult.Value);
        return filterComponentsResult.Error.Code switch
        {
            ErrorCodes.Kits.DOES_NOT_EXIST => NotFound(),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("AddComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> AddComponent(int kitId, int itemId)
    {
        var addingComponentVm = await _kitService.GetVmForAddingElementAsync(kitId, itemId);
        return View(addingComponentVm);
    }

    [Route("AddComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> AddComponent(ElementVm elementVm)
    {
        var validationResults = await _elemValidator.ValidateAsync(elementVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("AddComponent", elementVm);
        }

        await _kitService.AddElementAsync(elementVm);
        return RedirectToAction("Details", new { id = elementVm.KitDetailsVm.Id });
    }

    [Route("EditComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(int kitId, int itemId)
    {
        var addingComponentVm = await _kitService.GetVmForEditingElementAsync(kitId, itemId);
        return View(addingComponentVm);
    }

    [Route("EditComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(ElementVm elementVm)
    {
        var validationResults = await _elemValidator.ValidateAsync(elementVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View("AddComponent", elementVm);
        }

        await _kitService.EditElementAsync(elementVm);
        return RedirectToAction("Details", new { id = elementVm.KitDetailsVm.Id });
    }

    [Route("DeleteComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> DeleteComponent(int kitId, int itemId)
    {
        await _kitService.DeleteElementAsync(kitId, itemId);
        return RedirectToAction("Details", new { id = kitId });
    }
}