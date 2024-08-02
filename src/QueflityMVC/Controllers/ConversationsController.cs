using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Controllers;

[Route("Conversations")]
[Authorize]
public class ConversationsController(IMessageService messageService, ILogger<ConversationsController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var usersConversations = await messageService.GetUsersConversationsAsync();
        if (usersConversations.IsFailure)
            return RedirectToAction("Index", "Home");
        return View(usersConversations.Value);
    }
    
    [HttpGet]
    [Route("Details/{conversationId:int}")]
    public async Task<IActionResult> Details(int conversationId)
    {
        var conversationDetails = await messageService.GetConversationDetailsAsync(conversationId);
        if(conversationDetails.IsSuccess)
            return View(conversationDetails.Value);
        return conversationDetails.Error.Code switch 
        {
            ErrorCodes.Conversation.DOES_NOT_EXIST => StatusCode(404),
            ErrorCodes.Conversation.DOES_NOT_BELONG_TO_USER => StatusCode(403)
        };
    }
}