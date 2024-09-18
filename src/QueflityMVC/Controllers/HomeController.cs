using System.Diagnostics;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Web.Exceptions;
using QueflityMVC.Web.Models;

namespace QueflityMVC.Web.Controllers;

public class HomeController(
    ILogger<HomeController> logger,
    IProductService productService,
    IMessageService messageService,
    IValidator<FirstMessageInConversationVm> messageValidator)
    : Controller
{
    private readonly ILogger<HomeController> _logger = logger;

    public async Task<IActionResult> Index()
    {
        var dashboardVm = await productService.GetDashboardVmAsync();
        return View(dashboardVm);
    }

    [HttpGet]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(int id)
    {
        var contactVmResult = await messageService.GetContactVmAsync(id);
        if (contactVmResult.IsSuccess)
        {
            return View(contactVmResult.Value);
        }

        if (contactVmResult.Error.Code != ErrorCodes.Conversation.ALREADY_EXISTS)
        {
            return contactVmResult.Error.Code switch
            {
                ErrorCodes.User.EMAIL_NOT_VERIFIED => RedirectToPage("RegisterConfirmation",
                    new { email = User.FindFirstValue(ClaimTypes.Email) }),
                ErrorCodes.Product.DOES_NOT_EXIST => RedirectToAction("ProductNotFound", "Home")
            };
        }

        var conversationId = await messageService.GetConversationIdByProductAsync(id);
        if (conversationId.IsSuccess)
        {
            return RedirectToAction("Details", "Conversations", new { id = conversationId.Value });
        }

        _logger.LogError("Conversation already exists but could not get conversation id");
        throw new UnexpectedApplicationException();
    }


    [HttpPost]
    [Route("Contact")]
    [Authorize]
    public async Task<IActionResult> Contact(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        if (firstMessageInConversationVm.Product is null)
        {
            return RedirectToAction("ProductNotFound", "Home");
        }

        var productResult = await messageService.GetProductForContactVmAsync(firstMessageInConversationVm.Product.Id);
        if (productResult.IsFailure)
        {
            return productResult.Error.Code switch
            {
                ErrorCodes.Product.DOES_NOT_EXIST => RedirectToAction("ProductNotFound", "Home"),
                _ => throw new UnexpectedApplicationException()
            };
        }

        firstMessageInConversationVm = firstMessageInConversationVm with { Product = productResult.Value };

        var validationResults = await messageValidator.ValidateAsync(firstMessageInConversationVm);
        if (!validationResults.IsValid)
        {
            validationResults.AddToModelState(ModelState);
            return View(firstMessageInConversationVm);
        }

        var result = await messageService.StartConversationAsync(firstMessageInConversationVm);
        if (result.IsSuccess)
        {
            return RedirectToAction("Details", "Conversations", new { area = "", id = result.Value });
        }

        return result.Error.Code switch
        {
            ErrorCodes.User.EMAIL_NOT_VERIFIED => RedirectToPage("RegisterConfirmation",
                new { email = User.FindFirstValue(ClaimTypes.Email) }),
            ErrorCodes.Product.DOES_NOT_EXIST => RedirectToAction("ProductNotFound", "Home"),
            _ => throw new UnexpectedApplicationException()
        };
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int? id)
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            ErrorCode = id
        });
    }

    public IActionResult ProductNotFound()
    {
        return View();
    }
}