using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Web.Controllers
{
    [Route("Ingredients")]
    public class IngredientsController : Controller
    {
        private readonly IIngredientService _ingredientService;
        private readonly IValidator<IngredientDTO> _categoryValidator;

        public IngredientsController(IIngredientService ingredientService, IValidator<IngredientDTO> categoryValidator)
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

            var listVm = await _ingredientService.GetFilteredList(listIngredients);
            return View(listVm);
        }

        [Route("Create")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Create()
        {
            IngredientDTO freshIngredientDTO = new()
            {
                Id = 0,
                Name = string.Empty
            };

            return View(freshIngredientDTO);
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Create(IngredientDTO ingredientToAddDTO)
        {
            var validationResult = _categoryValidator.Validate(ingredientToAddDTO);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View("Create", ingredientToAddDTO);
            }

            _ingredientService.CreateIngredient(ingredientToAddDTO);

            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public IActionResult Edit(int id)
        {
            var ingredientVM = _ingredientService.GetIngredientVMForEdit(id);
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
        public IActionResult Edit(IngredientDTO ingredientToEditDTO)
        {
            var validationResult = _categoryValidator.Validate(ingredientToEditDTO);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View("Edit", ingredientToEditDTO);
            }

            _ingredientService.UpdateIngredient(ingredientToEditDTO);

            return RedirectToAction("Index");
        }

        [Route("Delete")]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Delete(int id)
        {
            _ingredientService.DeleteIngredient(id);
            return RedirectToAction("Index");
        }
    }
}