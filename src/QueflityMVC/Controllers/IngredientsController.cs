using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Web.Controllers;

[Route("Ingredients")]
public class IngredientsController : Controller
{
    private readonly IIngredientService _ingredientService;
    private readonly IValidator<IngredientVM> _categoryValidator;

    public IngredientsController(IIngredientService ingredientService, IValidator<IngredientVM> categoryValidator)
    {
        _ingredientService = ingredientService;
        _categoryValidator = categoryValidator;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index()
    {
        ListIngredientsVM listIngredientsVM = new()
        {
            Pagination = PaginationFactory.Default<IngredientForListVM>()
        };
        return await Index(listIngredientsVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListIngredientsVM listIngredients)
    {
        if (listIngredients is null)
            return BadRequest();
        listIngredients.NameFilter ??= string.Empty;

        var listVm = await _ingredientService.GetFilteredListAsync(listIngredients);
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
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(IngredientVM ingredientToAddVM)
    {
        var validationResult = _categoryValidator.Validate(ingredientToAddVM);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View("Create", ingredientToAddVM);
        }

        await _ingredientService.CreateIngredientAsync(ingredientToAddVM);
        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var ingredientVM = await _ingredientService.GetIngredientVMForEditAsync(id);
        if (ingredientVM is null)
        {
            return NotFound();
        }
        return View(ingredientVM);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(IngredientVM ingredientToEditVM)
    {
        var validationResult = await _categoryValidator.ValidateAsync(ingredientToEditVM);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View("Edit", ingredientToEditVM);
        }

        await _ingredientService.UpdateIngredientAsync(ingredientToEditVM);
        return RedirectToAction("Index");
    }

    [Route("Delete")]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        await _ingredientService.DeleteIngredientAsync(id);
        return RedirectToAction("Index");
    }
}