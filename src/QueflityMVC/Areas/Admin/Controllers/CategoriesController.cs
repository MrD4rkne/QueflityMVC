using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class CategoriesController(ICategoryService categoryService, IValidator<CategoryVm> categoryValidator)
    : Controller
{
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index()
    {
        ListCategoriesVm listCategoriesVm = new()
        {
            Pagination = PaginationFactory.Default<CategoryForListVm>()
        };
        return await Index(listCategoriesVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListCategoriesVm listCategoriesVm)
    {
        if (listCategoriesVm is null)
        {
            return BadRequest();
        }

        listCategoriesVm.NameFilter ??= string.Empty;

        var listVm = await categoryService.GetFilteredListAsync(listCategoriesVm);
        return View(listVm);
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryVm createCategoryVm)
    {
        var validationResult = await categoryValidator.ValidateAsync(createCategoryVm);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Create", createCategoryVm);
        }

        var result = await categoryService.CreateCategoryAsync(createCategoryVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { IsFailure: true, Error: { Code: ErrorCodes.Categories.DOES_NOT_EXIST } }:
                return NotFound();
            case { IsFailure: true, Error: { Code: ErrorCodes.Categories.DUPLICATED_NAME } }:
                ModelState.AddModelError(nameof(CategoryVm.Name), "Category with this name already exists.");
                return View();
            default:
                return RedirectToAction("Error", "Home", new { area = "" });
        }
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var vmForEdit = await categoryService.GetVmForEditAsync(id);
        if (vmForEdit.IsSuccess)
        {
            return View(vmForEdit.Value);
        }

        return vmForEdit.Error.Code switch
        {
            ErrorCodes.Categories.DOES_NOT_EXIST => NotFound(),
            _ => BadRequest()
        };
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(CategoryVm createCategoryVm)
    {
        var validationResult = await categoryValidator.ValidateAsync(createCategoryVm);

        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Edit", createCategoryVm);
        }

        var result = await categoryService.UpdateCategoryAsync(createCategoryVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { IsFailure: true, Error: { Code: ErrorCodes.Categories.DOES_NOT_EXIST } }:
                return NotFound();
            case { IsFailure: true, Error: { Code: ErrorCodes.Categories.DUPLICATED_NAME } }:
                ModelState.AddModelError(nameof(CategoryVm.Name), "Category with this name already exists.");
                return View();
            default:
                return this.RedirectToError();
        }
    }

    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await categoryService.DeleteCategoryAsync(id);
        }
        catch (InvalidOperationException invOpEx)
        {
            DeleteFailedCategoryVm deleteFailedVm = new()
            {
                CategoryId = id,
                Message = invOpEx.Message
            };

            return View(deleteFailedVm);
        }

        return RedirectToAction("Index");
    }

    public IActionResult ViewItems(int id)
    {
        return RedirectToAction("Index", "Items", new { categoryId = id });
    }
}