using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Web.Controllers
{
    [Route("Items")]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IValidator<ItemDTO> _itemValidator;
        private readonly IWebHostEnvironment _env;

        public ItemsController(IItemService itemService, IValidator<ItemDTO> itemValidator, IWebHostEnvironment env)
        {
            _itemService = itemService;
            _itemValidator = itemValidator;
            _env = env;
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

            ListItemsVM listVM = await _itemService.GetFilteredList(listItemsVM);
            return View(listVM);
        }

        [Route("Create")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Create(int? categoryId)
        {
            return View(_itemService.GetItemVMForAdding(categoryId));
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

                if (crEdObjItem.Categories is null)
                {
                    crEdObjItem.Categories = _itemService.GetCategoriesForSelectVM();
                }

                return View("Create", crEdObjItem);
            }

            int id = await _itemService.CreateItem(crEdObjItem.ItemVM, _env.ContentRootPath);

            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public IActionResult Edit(int id)
        {
            return View(_itemService.GetForEdit(id));
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

            await _itemService.UpdateItem(editItemVM.ItemVM, _env.ContentRootPath);

            return RedirectToAction("Index");
        }

        [Route("Delete")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id, _env.ContentRootPath);
            return RedirectToAction("Index");
        }

        [Route("Ingredients")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public IActionResult Ingredients(int id)
        {
            var ingredientsViewModel = _itemService.GetIngredientsForSelectionVM(id);
            if (ingredientsViewModel is null)
            {
                return NotFound();
            }
            if (ingredientsViewModel.AllIngredients.Count == 0)
            {
                return NoContent();
            }

            return View(ingredientsViewModel);
        }

        [Route("Ingredients")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public IActionResult Ingredients(ItemIngredientsSelectionVM selectionVM)
        {
            _itemService.UpdateItemIngredients(selectionVM);
            return RedirectToAction("Index");
        }
    }
}