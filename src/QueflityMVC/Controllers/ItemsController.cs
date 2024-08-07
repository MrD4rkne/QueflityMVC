﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Web.Exceptions;

namespace QueflityMVC.Web.Controllers;

[Route("Items")]
public class ItemsController : Controller
{
    private readonly IItemService _itemService;
    private readonly IValidator<ItemVm?> _itemValidator;

    public ItemsController(IItemService itemService, IValidator<ItemVm?> itemValidator)
    {
        _itemService = itemService;
        _itemValidator = itemValidator;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(int? categoryId)
    {
        ListItemsVm listItemsVm = new()
        {
            Pagination = PaginationFactory.Default<ItemForListVm>(),
            CategoryId = categoryId,
            NameFilter = string.Empty
        };
        return await Index(listItemsVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListItemsVm listItemsVm)
    {
        if (listItemsVm is null) return BadRequest();
        listItemsVm.NameFilter ??= string.Empty;

        var listVm = await _itemService.GetFilteredListAsync(listItemsVm);
        return View(listVm);
    }

    [Route("Create")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(int? categoryId)
    {
        var addingVm = await _itemService.GetItemVmForAddingAsync(categoryId);
        if (addingVm.IsSuccess) return View(addingVm.Value);

        return addingVm.Error.Code switch
        {
            ErrorCodes.Items.NO_CATEGORIES => RedirectToAction("NoCategories"),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(CrEdItemVm crEdObjItem)
    {
        var result = await _itemValidator.ValidateAsync(crEdObjItem.ItemVm);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            crEdObjItem.Categories ??= await _itemService.GetCategoriesForSelectVmAsync();
            return View("Create", crEdObjItem);
        }

        _ = await _itemService.CreateItemAsync(crEdObjItem.ItemVm);
        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var itemForEdit = await _itemService.GetForEditAsync(id);
        return View(itemForEdit);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(CrEdItemVm editItemVm)
    {
        var result = await _itemValidator.ValidateAsync(editItemVm.ItemVm);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View("Edit", editItemVm);
        }

        await _itemService.UpdateItemAsync(editItemVm.ItemVm);
        return RedirectToAction("Index");
    }

    [Route("Delete")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        var results = await _itemService.DeleteItemAsync(id);
        if (results.IsSuccess) return RedirectToAction("Index");

        return results.Error.Code switch
        {
            ErrorCodes.Items.DOES_NOT_EXIST => NotFound(),
            ErrorCodes.Items.IS_PART_OF_KIT => View(new DeleteFailedItemVm
            {
                ItemId = id, Message = "Item is part of a kit and cannot be deleted."
            }),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [Route("Components")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Components(int id)
    {
        var componentsViewModel = await _itemService.GetComponentsForSelectionVmAsync(id);
        if (componentsViewModel is null) return NotFound();
        if (componentsViewModel.AllComponents.Count == 0) return RedirectToAction("NoComponents");

        return View(componentsViewModel);
    }

    [Route("NoComponents")]
    [HttpGet]
    public IActionResult NoComponents()
    {
        return View();
    }

    [Route("NoCategories")]
    [HttpGet]
    public IActionResult NoCategories()
    {
        return View();
    }

    [Route("Components")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Components(ItemComponentsSelectionVm selectionVm)
    {
        await _itemService.UpdateItemComponentsAsync(selectionVm);
        return RedirectToAction("Index");
    }
}