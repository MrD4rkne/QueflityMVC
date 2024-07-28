using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Web.Common;

namespace QueflityMVC.Web.Controllers;

[Route("Conversations")]
[Authorize]
public class ConversationsController(IMessageService messageService, ILogger<ConversationsController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var userId = User.GetLoggedInUserId();
        var usersConversations = await messageService.GetUsersConversationsAsync(userId);
        if (usersConversations.IsFailure)
        {
            logger.LogError("Failed to get users conversations: {Error}", usersConversations.Error);
            return this.Error();
        }

        return View(usersConversations.Value);
    }
    
    [HttpGet]
    [Route("Details/{conversationId}")]
    public async Task<IActionResult> Details(int conversationId)
    {
        var userId = User.GetLoggedInUserId();
        var conversation = await messageService.GetConversationDetailsAsync(userId, conversationId);
        if (conversation.IsFailure)
        {
            logger.LogError("Failed to get conversation details: {Error}", conversation.Error);
            return this.Error();
        }

        return View(conversation.Value);
    }
}