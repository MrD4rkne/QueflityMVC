﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Kit;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Web.Models;

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
        var kitDetailsVm = await _kitService.GetDetailsVmAsync(id);
        return View(kitDetailsVm);
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var kitToEditVm = await _kitService.GetKitVmForEditAsync(id);
        return View(kitToEditVm);
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
        switch (results.Status)
        {
            case DeleteKitStatus.Success:
                return RedirectToAction("Index");

            case DeleteKitStatus.NotExist:
                return NotFound();

            case DeleteKitStatus.Exception:
                return RedirectToAction("Error", "Home", new ErrorViewModel());

            default:
                throw new NotImplementedException();
        }
    }

    [Route("ListItemsForComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(int kitId)
    {
        var itemsForComponentsListVm = await _kitService.GetFilteredListForComponentsAsync(kitId);
        return View(itemsForComponentsListVm);
    }

    [Route("ListItemsForComponent")]
    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(ListItemsForComponentsVm listItemsForComponentsVm)
    {
        var itemsForComponentsListVm = await _kitService.GetFilteredListForComponentsAsync(listItemsForComponentsVm);
        return View(itemsForComponentsListVm);
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
        var addingComponentVm = await _kitService.GetVmForEdittingElementAsync(kitId, itemId);
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