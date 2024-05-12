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
    private readonly IValidator<ComponentVm> _categoryValidator;
    private readonly IComponentService _componentService;

    public ComponentsController(IComponentService componentService, IValidator<ComponentVm> categoryValidator)
    {
        _componentService = componentService;
        _categoryValidator = categoryValidator;
    }

    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index()
    {
        ListComponentsVm listComponentsVm = new()
        {
            Pagination = PaginationFactory.Default<ComponentForListVm>()
        };
        return await Index(listComponentsVm);
    }

    [HttpPost]
    [Authorize(Policy = Policies.ENTITIES_LIST)]
    public async Task<IActionResult> Index(ListComponentsVm listComponents)
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
    public async Task<IActionResult> Create(ComponentVm componentToAddVm)
    {
        var validationResult = await _categoryValidator.ValidateAsync(componentToAddVm);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Create", componentToAddVm);
        }

        await _componentService.CreateComponentAsync(componentToAddVm);
        return RedirectToAction("Index");
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var componentVm = await _componentService.GetComponentVmForEditAsync(id);
        if (componentVm is null) return NotFound();
        return View(componentVm);
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(ComponentVm componentToEditVm)
    {
        var validationResult = await _categoryValidator.ValidateAsync(componentToEditVm);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Edit", componentToEditVm);
        }

        await _componentService.UpdateComponentAsync(componentToEditVm);
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