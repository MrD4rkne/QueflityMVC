using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Web.Controllers;

[Route("Categories")]
public class CategoriesController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IValidator<CategoryVm> _categoryValidator;

    public CategoriesController(ICategoryService categoryService, IValidator<CategoryVm> categoryValidator)
    {
        _categoryService = categoryService;
        _categoryValidator = categoryValidator;
    }

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
        if (listCategoriesVm is null) return BadRequest();
        listCategoriesVm.NameFilter ??= string.Empty;

        var listVm = await _categoryService.GetFilteredListAsync(listCategoriesVm);
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
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryVm createCategoryVm)
    {
        var result = await _categoryValidator.ValidateAsync(createCategoryVm);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View("Create", createCategoryVm);
        }

        _ = await _categoryService.CreateCategoryAsync(createCategoryVm);

        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var vmForEdit = await _categoryService.GetVmForEditAsync(id);
        return View(vmForEdit);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(CategoryVm createCategoryVm)
    {
        var result = await _categoryValidator.ValidateAsync(createCategoryVm);

        if (!result.IsValid)
        {
            result.AddToModelState(ModelState);
            return View("Edit", createCategoryVm);
        }

        _ = await _categoryService.UpdateCategoryAsync(createCategoryVm);
        return RedirectToAction("Index");
    }

    [Route("Delete")]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
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

    [Route("ViewItems")]
    public IActionResult ViewItems(int id)
    {
        return RedirectToAction("Index", "Items", new { categoryId = id });
    }
}