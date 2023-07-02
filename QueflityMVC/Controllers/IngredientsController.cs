using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Web.Controllers
{
    [Route("Ingredients")]
    public class IngredientsController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 2;

        private readonly IIngredientService _ingredientService;
        private readonly IValidator<IngredientDTO> _itemCategoryValidator;

        public IngredientsController(IIngredientService ingredientService, IValidator<IngredientDTO> itemCategoryValidator)
        {
            _ingredientService = ingredientService;
            _itemCategoryValidator = itemCategoryValidator;
        }

        public IActionResult Index()
        {
            return Index(null,string.Empty, DEFAULT_PAGE_SIZE, 1);
        }

        [HttpPost]
        public IActionResult Index(int? itemId,string nameFilter, int pageSize, int pageIndex)
        {
            if (nameFilter == null)
            {
                nameFilter = string.Empty;
            }
            if (pageSize <= 1)
            {
                pageSize = 2;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            var listVm = _ingredientService.GetFilteredList(itemId,nameFilter, pageSize, pageIndex);
            return View(listVm);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new IngredientDTO());
        }

        [Route("Create")]
        [HttpPost]
        public IActionResult Create(IngredientDTO ingredientToAddDTO)
        {
            var validationResult = _itemCategoryValidator.Validate(ingredientToAddDTO);

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
        public IActionResult Edit(IngredientDTO ingredientToEditDTO)
        {
            var validationResult = _itemCategoryValidator.Validate(ingredientToEditDTO);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return View("Edit", ingredientToEditDTO);
            }

            _ingredientService.UpdateIngredient(ingredientToEditDTO);

            return RedirectToAction("Index");
        }

        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            _ingredientService.DeleteIngredient(id);
            return RedirectToAction("Index");
        }
    }
}
