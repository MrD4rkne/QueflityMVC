using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Web.Controllers
{
    [Route("ItemCategories")]
    public class ItemCategoriesController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 2;

        private readonly IItemCategoryService _itemCategoryService;
        private readonly IValidator<ItemCategoryDTO> _itemCategoryValidator;

        public ItemCategoriesController(IItemCategoryService itemCategoryService, IValidator<ItemCategoryDTO> itemCategoryValidator)
        {
            _itemCategoryService = itemCategoryService;
            _itemCategoryValidator = itemCategoryValidator;
        }

        public IActionResult Index()
        {
            return Index(string.Empty, DEFAULT_PAGE_SIZE, 1);
        }

        [HttpPost]
        public IActionResult Index(string nameFilter, int pageSize, int pageIndex)
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

            var listVm = _itemCategoryService.GetFilteredList(nameFilter, pageSize, pageIndex);

            return View(listVm);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(new Application.ViewModels.ItemCategory.ItemCategoryDTO());
        }

        [Route("Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemCategoryDTO createItemCategoryVM)
        {
            ValidationResult result = await _itemCategoryValidator.ValidateAsync(createItemCategoryVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View("Create", createItemCategoryVM);
            }

            int id = _itemCategoryService.CreateItemCategory(createItemCategoryVM);

            return RedirectToAction("Index");
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(_itemCategoryService.GetVMForEdit(id));
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemCategoryDTO createItemCategoryVM)
        {
            ValidationResult result = await _itemCategoryValidator.ValidateAsync(createItemCategoryVM);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);
                return View("Edit", createItemCategoryVM);
            }

            _itemCategoryService.UpdateItemCategory(createItemCategoryVM);

            return RedirectToAction("Index");
        }

        [Route("Delete")]
        public IActionResult Delete(int id)
        {
            try
            {
                _itemCategoryService.DeleteItemCategory(id);
            }
            catch (InvalidOperationException invOpEx)
            {
                DeleteFailedItemCategoryVM deleteFailedVM = new()
                {
                    ItemCategoryId = id,
                    Message = invOpEx.Message
                };

                return View(deleteFailedVM);
            }
            return RedirectToAction("Index");
        }

        [Route("ViewItems")]
        public IActionResult ViewItems(int id)
        {
            return RedirectToAction("Index", "Items", new { itemCategoryId = id });
        }
    }
}
