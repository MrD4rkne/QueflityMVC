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

namespace QueflityMVC.Web.Controllers;

[Route("Kits")]
public class KitsController : Controller
{
    private readonly IKitService _kitService;
    private readonly IValidator<KitVM> _kitValidator;
    private readonly IValidator<ElementVM> _elemValidator;

    public KitsController(IKitService kitService, IValidator<KitVM> kitValidator, IValidator<ElementVM> elemValidator)
    {
        _kitService = kitService;
        _kitValidator = kitValidator;
        _elemValidator = elemValidator;
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

        ListKitsVM listVM = await _kitService.GetFilteredListAsync(listKitsVM);
        return View(listVM);
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
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(KitVM createKitVM, bool shouldRouteToDetails = false)
    {
        ValidationResult validationResults = await _kitValidator.ValidateAsync(createKitVM);

        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(this.ModelState);

            return View("Create", createKitVM);
        }

        int kitId = await _kitService.CreateKitAsync(createKitVM);

        if (shouldRouteToDetails)
            return RedirectToAction("Details", new { id = kitId });

        return RedirectToAction("Index");
    }

    [Route("Details")]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Details(int id)
    {
        var kitDetailsVM = await _kitService.GetDetailsVMAsync(id);
        return View(kitDetailsVM);
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        KitVM kitToEditVM = await _kitService.GetKitVMForEditAsync(id);
        return View(kitToEditVM);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(KitVM editedKitVM)
    {
        ValidationResult validationResults = await _kitValidator.ValidateAsync(editedKitVM);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(this.ModelState);
            return View("Edit", editedKitVM);
        }

        int kitId = await _kitService.EditKitAsync(editedKitVM);
        return RedirectToAction("Details", new { id = kitId });
    }

    [Route("ListItemsForComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(int setId)
    {
        var itemsForComponentsListVM = await _kitService.GetFilteredListForComponentsAsync(setId);
        return View(itemsForComponentsListVM);
    }

    [Route("ListItemsForComponent")]
    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> ListItemsForComponents(ListItemsForComponentsVM listItemsForComponentsVM)
    {
        var itemsForComponentsListVM = await _kitService.GetFilteredListForComponentsAsync(listItemsForComponentsVM);
        return View(itemsForComponentsListVM);
    }

    [Route("AddComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> AddComponent(int setId, int itemId)
    {
        var addingComponentVM = await _kitService.GetVMForAddingElementAsync(setId, itemId);
        return View(addingComponentVM);
    }

    [Route("AddComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> AddComponent(ElementVM elementVM)
    {
        ValidationResult validationResults = await _elemValidator.ValidateAsync(elementVM);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(this.ModelState);
            return View("AddComponent", elementVM);
        }

        await _kitService.AddElementAsync(elementVM);
        return RedirectToAction("Details", new { id = elementVM.KitDetailsVM.Id });
    }

    [Route("EditComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(int kitId, int itemId)
    {
        var addingComponentVM = await _kitService.GetVMForEdittingElementAsync(kitId, itemId);
        return View(addingComponentVM);
    }

    [Route("EditComponent")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> EditComponent(ElementVM elementVM)
    {
        ValidationResult validationResults = await _elemValidator.ValidateAsync(elementVM);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(this.ModelState);
            return View("AddComponent", elementVM);
        }

        await _kitService.EditElementAsync(elementVM);
        return RedirectToAction("Details", new { id = elementVM.KitDetailsVM.Id });
    }

    [Route("DeleteComponent")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> DeleteComponent(int kitId, int itemId)
    {
        await _kitService.DeleteElementAsync(kitId, itemId);
        return RedirectToAction("Details", new { id = kitId });
    }
}