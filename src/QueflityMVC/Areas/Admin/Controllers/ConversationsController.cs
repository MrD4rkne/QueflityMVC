using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Message;

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = Policies.CONVERSATIONS_RESPOND)]
public class ConversationsController(IMessageService messageService, ILogger<ConversationsController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersConversations = await messageService.GetAllButCurrentUserConversationsAsync();
        if (usersConversations.IsFailure)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(usersConversations.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserConversationsVm userConversationsVm)
    {
        if (userConversationsVm is null)
        {
            return BadRequest();
        }

        var usersConversations = await messageService.GetAllButCurrentUserConversationsAsync(userConversationsVm);
        if (usersConversations.IsFailure)
        {
            return RedirectToAction("Index", "Home");
        }

        return View(usersConversations.Value);
    }
}