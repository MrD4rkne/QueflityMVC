using System.Diagnostics;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    IProductEntityService purchasableEntityService,
    IMessageService messageService,
    IValidator<FirstMessageInConversationVm> messageValidator)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var dashboardVm = await purchasableEntityService.GetDashboardVmAsync();
        return View(dashboardVm);
    }

    [HttpGet]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(int id)
    {
        var contactVmResult = await messageService.GetContactVmAsync(id);
        if (contactVmResult.IsSuccess) return View(contactVmResult.Value);

        return contactVmResult.Error.Code switch
        {
            ErrorCodes.User.EMAIL_NOT_VERIFIED => RedirectToPage("RegisterConfirmation",
                new { email = User.FindFirstValue(ClaimTypes.Email) }),
            ErrorCodes.Product.DOES_NOT_EXIST => RedirectToAction("ProductNotFound", "Home")
        };
    }


    [HttpPost]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        var validationResults = await messageValidator.ValidateAsync(firstMessageInConversationVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View(firstMessageInConversationVm);
        }

        await messageService.StartConversationAsync(firstMessageInConversationVm);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult ProductNotFound()
    {
        return View();
    }
}