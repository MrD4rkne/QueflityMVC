using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Web.Controllers
{
    [Route("Kits")]
    public class KitsController : Controller
    {
        private readonly IKitService _kitService;
        private readonly IValidator<KitDTO> _kitValidator;
        private readonly IValidator<ElementDTO> _elemValidator;
        private readonly IWebHostEnvironment _env;

        public KitsController(IKitService kitService, IValidator<KitDTO> kitValidator, IValidator<ElementDTO> elemValidator, IWebHostEnvironment env)
        {
            _kitService = kitService;
            _kitValidator = kitValidator;
            _elemValidator = elemValidator;
            _env = env;
        }

        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public async Task<IActionResult> Index()
        {
            ListKitsVM listKitsVM = new()
            {
                Pagination = PaginationFactory.Default<KitForListVM>()
            };
            return await Index(listKitsVM);
        }

        [HttpPost]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public async Task<IActionResult> Index(ListKitsVM listKitsVM)
        {
            if (listKitsVM is null)
            {
                return BadRequest();
            }
            listKitsVM.NameFilter ??= string.Empty;

            ListKitsVM listVM = await _kitService.GetFilteredList(listKitsVM);
            return View(listVM);
        }

        [Route("Create")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult Create()
        {
            return View(_kitService.GetKitVMForAdding());
        }

        [Route("Create")]
        [HttpPost]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public async Task<IActionResult> Create(KitDTO createKitDTO, bool shouldRouteToDetails = false)
        {
            ValidationResult validationResults = await _kitValidator.ValidateAsync(createKitDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("Create", createKitDTO);
            }

            int kitId = await _kitService.CreateKit(createKitDTO, _env.ContentRootPath);

            if (shouldRouteToDetails)
                return RedirectToAction("Details", new { id = kitId });

            return RedirectToAction("Index");
        }

        [Route("Details")]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public IActionResult Details(int id)
        {
            var kitDetailsVM = _kitService.GetDetailsVM(id);

            return View(kitDetailsVM);
        }

        [Route("Edit")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public IActionResult Edit(int id)
        {
            KitDTO kitToEditVM = _kitService.GetKitVMForEdit(id);

            return View(kitToEditVM);
        }

        [Route("Edit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_EDIT)]
        public async Task<IActionResult> Edit(KitDTO editedKitDTO)
        {
            ValidationResult validationResults = await _kitValidator.ValidateAsync(editedKitDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("Edit", editedKitDTO);
            }

            int kitId = await _kitService.EditKit(editedKitDTO, _env.ContentRootPath);

            return RedirectToAction("Details", new { id = kitId });
        }

        [Route("ListItemsForComponent")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public async Task<IActionResult> ListItemsForComponents(int setId)
        {
            var itemsForComponentsListVM = await _kitService.GetFilteredListForComponents(setId);
            return View(itemsForComponentsListVM);
        }

        [Route("ListItemsForComponent")]
        [HttpPost]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public async Task<IActionResult> ListItemsForComponents(ListItemsForComponentsVM listItemsForComponentsVM)
        {
            var itemsForComponentsListVM = await _kitService.GetFilteredListForComponents(listItemsForComponentsVM);
            return View(itemsForComponentsListVM);
        }

        [Route("AddComponent")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult AddComponent(int setId, int itemId)
        {
            var addingComponentVM = _kitService.GetVMForAddingElement(setId, itemId);
            return View(addingComponentVM);
        }

        [Route("AddComponent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_LIST)]
        public async Task<IActionResult> AddComponent(ElementDTO elementDTO)
        {
            ValidationResult validationResults = await _elemValidator.ValidateAsync(elementDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("AddComponent", elementDTO);
            }

            _kitService.AddElement(elementDTO);
            return RedirectToAction("Details", new { id = elementDTO.KitDetailsVM.Id });
        }

        [Route("EditComponent")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult EditComponent(int kitId, int itemId)
        {
            var addingComponentVM = _kitService.GetVMForEdittingElement(kitId, itemId);
            return View(addingComponentVM);
        }

        [Route("EditComponent")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public async Task<IActionResult> EditComponent(ElementDTO elementDTO)
        {
            ValidationResult validationResults = await _elemValidator.ValidateAsync(elementDTO);

            if (!validationResults.IsValid)
            {
                validationResults.AddToModelState(this.ModelState);

                return View("AddComponent", elementDTO);
            }

            _kitService.EditElement(elementDTO);
            return RedirectToAction("Details", new { id = elementDTO.KitDetailsVM.Id });
        }

        [Route("DeleteComponent")]
        [HttpGet]
        [Authorize(Policy = Policies.ENTITIES_CREATE)]
        public IActionResult DeleteComponent(int kitId, int itemId)
        {
            _kitService.DeleteElement(kitId, itemId);
            return RedirectToAction("Details", new { id = kitId });
        }
    }
}