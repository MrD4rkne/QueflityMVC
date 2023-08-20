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
        public async Task<IActionResult> Create(ItemSetDTO createItemSetDTO, bool shouldRouteToDetails = false)
        {
            ValidationResult validationResults = await _itemSetValidator.ValidateAsync(createItemSetDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("Create", createItemSetDTO);
            }

            int itemSetId = await _itemSetService.CreateItemSet(createItemSetDTO, _env.ContentRootPath);

            if (shouldRouteToDetails)
                return RedirectToAction("Details", new { id = itemSetId});

            return RedirectToAction("Index");
        }

        [Route("Details")]
        public IActionResult Details(int id)
        {
            var itemSetDetailsVM = _itemSetService.GetDetailsVM(id);

            return View(itemSetDetailsVM);
        }

        [Route("Edit")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ItemSetDTO itemSetToEditVM = _itemSetService.GetItemSetVMForEdit(id);

            return View(itemSetToEditVM);
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(ItemSetDTO editedItemSetDTO) {

            ValidationResult validationResults = await _itemSetValidator.ValidateAsync(editedItemSetDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("Edit", editedItemSetDTO);
            }

            int itemSetId = await _itemSetService.EditItemSet(editedItemSetDTO, _env.ContentRootPath);

            return RedirectToAction("Details", new {id = itemSetId});
        }

        // TODO: Add view & logic for adding new components
        [Route("AddComponent")]
        [HttpGet]
        public IActionResult AddComponent(int id)
        {
            throw new NotImplementedException();
        }
    }
}
