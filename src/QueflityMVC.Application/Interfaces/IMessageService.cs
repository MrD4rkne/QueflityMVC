using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Interfaces;

public interface IMessageService
{
    Task<Result<FirstMessageInConversationVm>> GetContactVmAsync(int id, string email);

    Task<Result> StartConversationAsync(FirstMessageInConversationVm firstMessageInConversationVm, string userId);

    Task<Result<UserConversationsVm>> GetUsersConversationsAsync(string userId);
    
    Task<Result<ConversationVm>> GetConversationDetailsAsync(string? userId, int conversationId);
}