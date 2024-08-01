using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
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
    
    [HttpGet]
    [Route("Details/{conversationId:int}")]
    public async Task<IActionResult> Details(int conversationId)
    {
        var userId = User.GetLoggedInUserId();
        var conversationDetails = await messageService.GetConversationDetailsAsync(userId, conversationId);
        if(conversationDetails.IsSuccess)
            return View(conversationDetails.Value);
        return conversationDetails.Error.Code switch 
        {
            ErrorCodes.Conversation.DOES_NOT_EXIST => StatusCode(404),
            ErrorCodes.Conversation.DOES_NOT_BELONG_TO_USER => StatusCode(403)
        };
    }
}