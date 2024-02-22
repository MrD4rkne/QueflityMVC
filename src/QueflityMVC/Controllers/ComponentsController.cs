using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Web.Controllers;

[Route("Components")]
public class ComponentsController : Controller
{
    private readonly IComponentService _componentService;
    private readonly IValidator<ComponentVM> _categoryValidator;

    public ComponentsController(IComponentService componentService, IValidator<ComponentVM> categoryValidator)
    {
        _componentService = componentService;
        _categoryValidator = categoryValidator;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index()
    {
        ListComponentsVM listComponentsVM = new()
        {
            Pagination = PaginationFactory.Default<ComponentForListVM>()
        };
        return await Index(listComponentsVM);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListComponentsVM listComponents)
    {
        if (listComponents is null)
            return BadRequest();
        listComponents.NameFilter ??= string.Empty;

        var listVm = await _componentService.GetFilteredListAsync(listComponents);
        return View(listVm);
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
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Create(ComponentVM componentToAddVM)
    {
        var validationResult = _categoryValidator.Validate(componentToAddVM);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View("Create", componentToAddVM);
        }

        await _componentService.CreateComponentAsync(componentToAddVM);
        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var componentVM = await _componentService.GetComponentVMForEditAsync(id);
        if (componentVM is null)
        {
            return NotFound();
        }
        return View(componentVM);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(ComponentVM componentToEditVM)
    {
        var validationResult = await _categoryValidator.ValidateAsync(componentToEditVM);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(this.ModelState);
            return View("Edit", componentToEditVM);
        }

        await _componentService.UpdateComponentAsync(componentToEditVM);
        return RedirectToAction("Index");
    }

    [Route("Delete")]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        await _componentService.DeleteComponentAsync(id);
        return RedirectToAction("Index");
    }
}