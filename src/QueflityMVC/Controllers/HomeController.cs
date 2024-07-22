using System.Diagnostics;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Web.Common;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMessageService _messageService;
    private readonly IValidator<MessageVm> _messageValidator;
    private readonly IProductEntityService _purchasableEntityService;

    public HomeController(ILogger<HomeController> logger, IProductEntityService purchasableEntityService,
        IMessageService messageService, IValidator<MessageVm> messageValidator)
    {
        _logger = logger;
        _purchasableEntityService = purchasableEntityService;
        _messageValidator = messageValidator;
        _messageService = messageService;
    }

    public async Task<IActionResult> Index()
    {
        var dashboardVm = await _purchasableEntityService.GetDashboardVmAsync();
        return View(dashboardVm);
    }

    [HttpGet]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(int id)
    {
        var contactVmResult = await _messageService.GetContactVmAsync(id, User.GetLoggedInUserId());
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
    public async Task<IActionResult> Contact(MessageVm messageVm)
    {
        var validationResults = await _messageValidator.ValidateAsync(messageVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View(messageVm);
        }

        await _messageService.SendMessageAsync(messageVm, User.GetLoggedInUserId());
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