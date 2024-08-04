using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Web.Chat;

[Authorize]
public class MessageHub(IMessageService messageService, IUserContext userContext) : Hub
{
    public async Task JoinGroup(int conversationId)
    {
        if (await messageService.CanAccessConversation(conversationId))
            await Groups.AddToGroupAsync(Context.ConnectionId, conversationId.ToString());
    }

    public async Task SendMessageToGroup(int conversationId, string message)
    {
        var result = await messageService.SendMessage(conversationId, message);
        if (result.IsSuccess)
        {
            await Clients.Group(conversationId.ToString()).SendAsync("ReceiveMessage", result.Value);
        }
    }
}