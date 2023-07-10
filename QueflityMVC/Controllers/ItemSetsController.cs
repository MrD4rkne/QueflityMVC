using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Web.Controllers
{
    [Route("ItemSets")]
    public class ItemSetsController : Controller
    {
        private const int DEFAULT_PAGE_SIZE = 2;

        private readonly IItemSetService _itemSetService;
        private readonly IValidator<ItemSetDTO> _itemSetValidator;
        private readonly IWebHostEnvironment _env;

        public ItemSetsController(IItemSetService itemSetService, IValidator<ItemSetDTO> itemSetValidator, IWebHostEnvironment env)
        {
            _itemSetService = itemSetService;
            _itemSetValidator = itemSetValidator;
            _env = env;
        }

        public IActionResult Index()
        {
            return Index(string.Empty, DEFAULT_PAGE_SIZE, 1);
        }

        [HttpPost]
        public IActionResult Index(string nameFilter, int pageSize, int pageIndex)
        {
            nameFilter ??= string.Empty;
            if (pageSize <= 1)
            {
                pageSize = 2;
            }
            if (pageIndex < 1)
            {
                pageIndex = 1;
            }

            ListItemSetsVM listVM = _itemSetService.GetFilteredList(nameFilter, pageSize, pageIndex);

            return View(listVM);
        }

        [Route("Create")]
        [HttpGet]
        public IActionResult Create()
        {
            return View(_itemSetService.GetItemSetVMForAdding());
        }

        [Route("Create")]
        [HttpPost]
        public async Task<IActionResult> Create(ItemSetDTO createItemSetDTO, bool routeToDetails = false)
        {
            ValidationResult result = await _itemSetValidator.ValidateAsync(createItemSetDTO);

            if (!result.IsValid)
            {
                result.AddToModelState(this.ModelState);

                return View("Create", createItemSetDTO);
            }

            int itemSetId = await _itemSetService.CreateItemSet(createItemSetDTO, _env.ContentRootPath);

            if (routeToDetails)
                return RedirectToAction("Details", new { id = itemSetId});

            return RedirectToAction("Index");
        }

        [Route("Details")]
        public IActionResult Details(int id)
        {
            var itemSetDetailsVM = _itemSetService.GetDetailsVM(id);

            return View(itemSetDetailsVM);
        }

        //[Route("Edit")]
        //[HttpGet]
        //public IActionResult Edit(int id)
        //{
        //    return View(_itemSetService.GetForEdit(id));
        //}

        //[Route("Edit")]
        //[HttpPost]
        //public async Task<IActionResult> Edit(CrEdItemVM editItemVM)
        //{
        //    ValidationResult result = await _itemValidator.ValidateAsync(editItemVM.ItemVM);

        //    if (!result.IsValid)
        //    {
        //        result.AddToModelState(this.ModelState);
        //        return View("Edit", editItemVM);
        //    }

        //    await _itemSetService.UpdateItem(editItemVM.ItemVM, _env.ContentRootPath);

        //    return RedirectToAction("Index");
        //}

        //[Route("Delete")]
        //[HttpGet]
        //public IActionResult Delete(int id)
        //{
        //    _itemSetService.DeleteItem(id,_env.ContentRootPath);
        //    return RedirectToAction("Index");
        //}

        //[Route("Ingredients")]
        //[HttpGet]
        //public IActionResult Ingredients(int id)
        //{
        //    var ingredientsViewModel = _itemSetService.GetIngredientsForSelectionVM(id);
        //    return View(ingredientsViewModel);
        //}

        //[Route("Ingredients")]
        //[HttpPost]
        //public IActionResult Ingredients(ItemIngredientsSelectionVM selectionVM)
        //{
        //    _itemSetService.UpdateItemIngredients(selectionVM);
        //    return RedirectToAction("Index");
        //}
    }
}
