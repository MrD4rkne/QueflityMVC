using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Web.Exceptions;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ItemsController(
    IItemService itemService,
    IValidator<ItemVm?> itemValidator,
    ILogger<ItemsController> logger)
    : Controller
{
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
        if (listItemsVm is null)
        {
            return BadRequest();
        }

        listItemsVm.NameFilter ??= string.Empty;

        var listVm = await itemService.GetFilteredListAsync(listItemsVm);
        return View(listVm);
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(int? categoryId)
    {
        var addingVm = await itemService.GetItemVmForAddingAsync(categoryId);
        if (addingVm.IsSuccess)
        {
            return View(addingVm.Value);
        }

        return addingVm.Error.Code switch
        {
            ErrorCodes.Items.NO_CATEGORIES => RedirectToAction("NoCategories"),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(ManageItemVm manageObjItem)
    {
        var result = await itemValidator.ValidateAsync(manageObjItem.ItemVm);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            manageObjItem.Categories ??= await itemService.GetCategoriesForSelectVmAsync();
            return View("Create", manageObjItem);
        }

        var addResult = await itemService.CreateItemAsync(manageObjItem.ItemVm);
        switch (addResult)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { Error.Code: ErrorCodes.Categories.DOES_NOT_EXIST }:
                ModelState.AddModelError(nameof(manageObjItem.ItemVm.CategoryId),
                    "Category does not exist. Please select a valid category.");
                return View("Create", manageObjItem);
            case { Error.Code: ErrorCodes.Files.FILE_UPLOAD_FAILED }:
                ModelState.AddModelError(nameof(ManageItemVm.ItemVm.Image.FormFile),
                    "File upload failed. Please try again.");
                return View("Create", manageObjItem);
            default:
                logger.LogError("Unexpected error occurred while creating item: {item} with error: {error}",
                    manageObjItem.ItemVm, addResult.Error);
                return RedirectToAction("Error", "Home", new { area = "" });
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var itemForEdit = await itemService.GetForEditAsync(id);
        if (itemForEdit.IsSuccess)
        {
            return View(itemForEdit.Value);
        }

        return itemForEdit.Error.Code switch
        {
            ErrorCodes.Items.DOES_NOT_EXIST => NotFound(),
            _ => this.RedirectToError()
        };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(ManageItemVm editItemVm)
    {
        var result = await itemValidator.ValidateAsync(editItemVm.ItemVm);
        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View("Edit", editItemVm);
        }

        var updateResult = await itemService.UpdateItemAsync(editItemVm.ItemVm);
        switch (updateResult)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { Error: { Code: ErrorCodes.Items.DOES_NOT_EXIST } }:
                return NotFound();
            case { Error: { Code: ErrorCodes.Categories.DOES_NOT_EXIST } }:
                ModelState.AddModelError(nameof(editItemVm.ItemVm.CategoryId),
                    "Category does not exist. Please select a valid category.");
                return View("Edit", editItemVm);
            default:
                logger.LogError("Unexpected error occurred while updating item: {item} with error: {error}",
                    editItemVm.ItemVm, updateResult.Error);
                return RedirectToAction("Error", "Home", new { area = "" });
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id, int? categoryId)
    {
        var results = await itemService.DeleteItemAsync(id);
        switch (results)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index", new { categoryId });
            case { Error: { Code: ErrorCodes.Items.DOES_NOT_EXIST } }:
                return NotFound();
            case { Error: { Code: ErrorCodes.Items.IS_PART_OF_KIT } }:
                return View(new DeleteFailedItemVm
                {
                    ItemId = id, Message = "Item is part of a kit and cannot be deleted.", CategoryId = categoryId
                });
            default:
                logger.LogError("Unexpected error occurred while deleting item: {id} with error: {error}",
                    id, results.Error);
                return RedirectToAction("Error", "Home", new { area = "" });
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Components(int id)
    {
        var componentsViewModel = await itemService.GetComponentsForSelectionVmAsync(id);
        if (componentsViewModel.IsSuccess)
        {
            if (componentsViewModel.Value.AllComponents.Count == 0)
            {
                return RedirectToAction("NoComponents");
            }

            return View(componentsViewModel.Value);
        }

        return componentsViewModel.Error.Code switch
        {
            ErrorCodes.Items.DOES_NOT_EXIST => NotFound(),
            _ => throw new UnexpectedApplicationException()
        };
    }

    [HttpGet]
    public IActionResult NoComponents()
    {
        return View();
    }

    [HttpGet]
    public IActionResult NoCategories()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Components(ItemComponentsSelectionVm selectionVm)
    {
        await itemService.UpdateItemComponentsAsync(selectionVm);
        return RedirectToAction("Index");
    }
}