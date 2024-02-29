using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Exceptions.UseCases;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

[Route("Items")]
public class ItemsController : Controller
{
    private readonly IItemService _itemService;
    private readonly IValidator<ItemVM> _itemValidator;

    public ItemsController(IItemService itemService, IValidator<ItemVM> itemValidator)
    {
        _itemService = itemService;
        _itemValidator = itemValidator;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(int? categoryId)
    {
        ListItemsVM listItemsVM = new()
        {
            Pagination = PaginationFactory.Default<ItemForListVM>(),
            CategoryId = categoryId,
            NameFilter = string.Empty
        };
        return await Index(listItemsVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListItemsVM listItemsVM)
    {
        if (listItemsVM is null)
        {
            return BadRequest();
        }
        listItemsVM.NameFilter ??= string.Empty;

        ListItemsVM listVM = await _itemService.GetFilteredListAsync(listItemsVM);
        return View(listVM);
    }

    [Route("Create")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(int? categoryId)
    {
        try
        {
            var addingVm = await _itemService.GetItemVMForAddingAsync(categoryId);
            return View(addingVm);
        }
        catch (NoCategoriesException)
        {
            return RedirectToAction("NoCategories", "Items");
        }
    }

    [Route("Create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(CrEdItemVM crEdObjItem)
    {
        ValidationResult result = await _itemValidator.ValidateAsync(crEdObjItem.ItemVM);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            crEdObjItem.Categories ??= await _itemService.GetCategoriesForSelectVMAsync();
            return View("Create", crEdObjItem);
        }

        _ = await _itemService.CreateItemAsync(crEdObjItem.ItemVM);
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
    public async Task<IActionResult> Edit(CrEdItemVM editItemVM)
    {
        ValidationResult result = await _itemValidator.ValidateAsync(editItemVM.ItemVM);
        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View("Edit", editItemVM);
        }

        await _itemService.UpdateItemAsync(editItemVM.ItemVM);
        return RedirectToAction("Index");
    }

    [Route("Delete")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        var results = await _itemService.DeleteItemAsync(id);
        switch (results.Status)
        {
            case Application.Results.Item.DeleteItemStatus.Success:
                return RedirectToAction("Index");
            case Application.Results.Item.DeleteItemStatus.NotExist:
                return NotFound();
            case Application.Results.Item.DeleteItemStatus.ItemIsPartOfKit:
                return View(new DeleteFailedItemVM() { 
                    ItemId = id,
                    Message = "Item is part of a kit and cannot be deleted."
                });
            case Application.Results.Item.DeleteItemStatus.Exception:
                throw results.Exception!;
            default:
                throw new NotImplementedException();
        }
    }

    [Route("Components")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Components(int id)
    {
        var componentsViewModel = await _itemService.GetComponentsForSelectionVMAsync(id);
        if (componentsViewModel is null)
        {
            return NotFound();
        }
        if (componentsViewModel.AllComponents.Count == 0)
        {
            return RedirectToAction("NoComponents");
        }

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
    public async Task<IActionResult> Components(ItemComponentsSelectionVM selectionVM)
    {
        await _itemService.UpdateItemComponentsAsync(selectionVM);
        return RedirectToAction("Index");
    }
}