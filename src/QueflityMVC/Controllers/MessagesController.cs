using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Controllers;

[Route("Messages")]
[Authorize]
public class MessagesController(IMessageService messageService) : Controller
{
    public async Task<IActionResult> Index()
    {
        var userId = User.GetLoggedInUserId();
        var usersConversations = await messageService.GetUsersConversationsAsync(userId);
        if (usersConversations.IsFailure)
            return RedirectToAction("Index", "Home");
        return View(usersConversations.Value);
    }
}