using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Helpers;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Web.Controllers
{
    [Route("Items")]
    public class ItemsController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 2;

        private readonly IItemService _itemService;
        private readonly IValidator<ItemDTO> _itemValidator;
        private readonly IWebHostEnvironment _env;

        public ItemsController(IItemService itemService, IValidator<ItemDTO> itemValidator, IWebHostEnvironment env)
        {
            _itemService = itemService;
            _itemValidator = itemValidator;
            _env = env;
        }

        public IActionResult Index(int? itemCategoryId)
        {
            return Index(itemCategoryId, string.Empty, DEFAULT_PAGE_SIZE, 1);
        }

        [HttpPost]
        public IActionResult Index(int? itemCategoryId, string nameFilter, int pageSize, int pageIndex)
        {
            if (nameFilter == null)
                nameFilter = string.Empty;
            if (pageSize <= 1)
                pageSize = 2;
            if (pageIndex < 1)
                pageIndex = 1;

            ListItemsVM listVM;
            if (itemCategoryId.HasValue)
                listVM = _itemService.GetFilteredList(itemCategoryId.Value, nameFilter, pageSize, pageIndex);
            else
                listVM = _itemService.GetFilteredList(nameFilter, pageSize, pageIndex);

            listVM.ItemCategoryId = itemCategoryId;

            return View(listVM);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create(int? categoryId)
        {
            return View(_itemService.GetItemVMForAdding(categoryId));
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(CrEdItemVM crEdObjItem)
        {
            ValidationResult result = await _itemValidator.ValidateAsync(crEdObjItem.ItemVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                if (crEdObjItem.ItemCategories == null)
                    crEdObjItem.ItemCategories = _itemService.GetItemCategoriesForSelectVM();

                return View("Create", crEdObjItem);
            }

            int id = await _itemService.CreateItem(crEdObjItem.ItemVM, _env.ContentRootPath);

            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_itemService.GetForEdit(id));
        }

        [Route("Edit")]
        [HttpPost]
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
        public IActionResult Delete(int id)
        {
            _itemService.DeleteItem(id,_env.ContentRootPath);

            return RedirectToAction("Index");
        }

        [Route("Ingredients")]
        [HttpGet]
        public IActionResult Ingredients(int id)
        {
            var ingredientsViewModel = _itemService.GetIngredientsForSelectionVM(id);
            return View(ingredientsViewModel);
        }

        [Route("Ingredients")]
        [HttpPost]
        public IActionResult Ingredients(ItemIngredientsSelectionVM selectionVM)
        {
            //var ingredientsViewModel = _itemService.GetIngredientsForSelectionVM(id);
            _itemService.UpdateItemIngredients(selectionVM);
            return RedirectToAction("Index");
        }
    }
}
