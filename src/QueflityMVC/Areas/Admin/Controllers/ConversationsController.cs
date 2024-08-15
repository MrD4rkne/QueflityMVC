#region

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Message;

#endregion

namespace QueflityMVC.Web.Areas.Admin.Controllers;

[Authorize(Policy = Policies.CONVERSATIONS_RESPOND)]
public class ConversationsController(IMessageService messageService, ILogger<ConversationsController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersConversations = await messageService.GetAllConversationsAsync();
        if (usersConversations.IsFailure)
            return RedirectToAction("Index", "Home");
        return View(usersConversations.Value);
    }

    [HttpPost]
    public async Task<IActionResult> Index(UserConversationsVm userConversationsVm)
    {
        if (userConversationsVm.PaginatedConversations is null)
            return BadRequest();

        var usersConversations = await messageService.GetAllConversationsAsync(userConversationsVm);
        if (usersConversations.IsFailure)
            return RedirectToAction("Index", "Home");

        return View(usersConversations.Value);
    }
}