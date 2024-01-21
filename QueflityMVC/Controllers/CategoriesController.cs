using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Web.Controllers
{
    [Route("Categories")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IValidator<CategoryDTO> _categoryValidator;

        public CategoriesController(ICategoryService categoryService, IValidator<CategoryDTO> categoryValidator)
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

            var listVm = await _categoryService.GetFilteredList(listCategoriesVM);
            return View(listVm);
        }

        [Route("Create")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Create()
        {
            CategoryDTO defaultCategoryDTO = _categoryService.GetVMForCreate();
            return View(defaultCategoryDTO);
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO createCategoryVM)
        {
            ValidationResult result = await _categoryValidator.ValidateAsync(createCategoryVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View("Create", createCategoryVM);
            }

            int id = _categoryService.CreateCategory(createCategoryVM);

            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public IActionResult Edit(int id)
        {
            return View(_categoryService.GetVMForEdit(id));
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public async Task<IActionResult> Edit(CategoryDTO createCategoryVM)
        {
            ValidationResult result = await _categoryValidator.ValidateAsync(createCategoryVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View("Edit", createCategoryVM);
            }

            _categoryService.UpdateCategory(createCategoryVM);

            return RedirectToAction("Index");
        }

        [Route("Delete")]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Delete(int id)
        {
            try
            {
                _categoryService.DeleteCategory(id);
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
}