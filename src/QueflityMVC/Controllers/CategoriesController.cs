using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
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
    private readonly IValidator<CategoryVM> _categoryValidator;

    public CategoriesController(ICategoryService categoryService, IValidator<CategoryVM> categoryValidator)
    {
        _categoryService = categoryService;
        _categoryValidator = categoryValidator;
    }

    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index()
    {
        ListCategoriesVM listCategoriesVM = new()
        {
            Pagination = PaginationFactory.Default<CategoryForListVM>()
        };
        return await Index(listCategoriesVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListCategoriesVM listCategoriesVM)
    {
        if (listCategoriesVM is null)
        {
            return BadRequest();
        }
        listCategoriesVM.NameFilter ??= string.Empty;

        var listVm = await _categoryService.GetFilteredListAsync(listCategoriesVM);
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
    public async Task<IActionResult> Create(CategoryVM createCategoryVM)
    {
        ValidationResult result = await _categoryValidator.ValidateAsync(createCategoryVM);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View("Create", createCategoryVM);
        }

        _ = await _categoryService.CreateCategoryAsync(createCategoryVM);

        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var vmForEdit = await _categoryService.GetVMForEditAsync(id);
        return View(vmForEdit);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(CategoryVM createCategoryVM)
    {
        ValidationResult result = await _categoryValidator.ValidateAsync(createCategoryVM);

        if (!result.IsValid)
        {
            result.AddToModelState(this.ModelState);
            return View("Edit", createCategoryVM);
        }

        _ =await _categoryService.UpdateCategoryAsync(createCategoryVM);
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
            DeleteFailedCategoryVM deleteFailedVM = new()
            {
                CategoryId = id,
                Message = invOpEx.Message
            };

            return View(deleteFailedVM);
        }
        return RedirectToAction("Index");
    }

    [Route("ViewItems")]
    public IActionResult ViewItems(int id)
    {
        return RedirectToAction("Index", "Items", new { categoryId = id });
    }
}