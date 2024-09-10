using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
public class ComponentsController(
    IComponentService componentService,
    ILogger<ComponentsController> logger,
    IValidator<ComponentVm> categoryValidator)
    : Controller
{
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
        {
            return BadRequest();
        }

        listComponents.NameFilter ??= string.Empty;

        var listVm = await componentService.GetFilteredListAsync(listComponents);
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
        var validationResult = await categoryValidator.ValidateAsync(componentToAddVm);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Create", componentToAddVm);
        }

        var result = await componentService.CreateComponentAsync(componentToAddVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { IsSuccess: false, Error.Code: ErrorCodes.Components.DUPLICATED_NAME }:
                ModelState.AddModelError(nameof(ComponentVm.Name),
                    "Component with this name already exists. Name must be unique.");
                return View();
        }

        return this.RedirectToError();
    }

    [Route("Edit")]
    [HttpGet]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(int id)
    {
        var result = await componentService.GetComponentVmForEditAsync(id);
        switch (result)
        {
            case { IsSuccess: true }:
                return View(result.Value);
            case { IsFailure: true, Error.Code: ErrorCodes.Components.DOES_NOT_EXIST }:
                return NotFound();
            default:
                logger.LogError("Error while getting component with id {id}: {error}", id, result.Error);
                return this.RedirectToError();
        }
    }

    [Route("Edit")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    [Authorize(Policy = Policies.ENTITIES_EDIT)]
    public async Task<IActionResult> Edit(ComponentVm componentToEditVm)
    {
        var validationResult = await categoryValidator.ValidateAsync(componentToEditVm);
        if (!validationResult.IsValid)
        {
            validationResult.AddToModelState(ModelState);
            return View("Edit", componentToEditVm);
        }

        var result = await componentService.UpdateComponentAsync(componentToEditVm);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { IsFailure: true, Error.Code: ErrorCodes.Components.DUPLICATED_NAME }:
                ModelState.AddModelError(nameof(ComponentVm.Name),
                    "Component with this name already exists. Name must be unique.");
                return View();
            case { IsFailure: true, Error.Code: ErrorCodes.Components.DOES_NOT_EXIST }:
                return NotFound();
            default:
                logger.LogError("Error while updating component with id {id}: {error}", componentToEditVm.Id,
                    result.Error);
                return this.RedirectToError();
        }
    }

    [Route("Delete")]
    [Authorize(Policy = Policies.ENTITIES_CREATE)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await componentService.DeleteComponentAsync(id);
        switch (result)
        {
            case { IsSuccess: true }:
                return RedirectToAction("Index");
            case { IsFailure: true, Error.Code: ErrorCodes.Components.DOES_NOT_EXIST }:
                return NotFound();
            default:
                logger.LogError("Error while deleting component with id {id}: {error}", id, result.Error);
                return this.RedirectToError();
        }
    }
}