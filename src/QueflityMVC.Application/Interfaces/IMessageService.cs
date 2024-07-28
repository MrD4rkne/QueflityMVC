using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using MessageVm = QueflityMVC.Application.ViewModels.Other.MessageVm;

namespace QueflityMVC.Application.Interfaces;

public interface IMessageService
{
    Task<Result<MessageVm>> GetContactVmAsync(int id, string email);

    Task<Result> StartConversationAsync(MessageVm messageVm, string userId);

    Task<Result<UserConversationsVm>> GetUsersConversationsAsync(string userId);
    
    Task<Result<ConversationVm>> GetConversationDetailsAsync(string? userId, int conversationId);
}