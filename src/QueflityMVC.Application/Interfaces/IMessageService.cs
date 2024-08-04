using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Interfaces;

public interface IMessageService
{
    Task<Result<FirstMessageInConversationVm>> GetContactVmAsync(int id);

    Task<Result> StartConversationAsync(FirstMessageInConversationVm firstMessageInConversationVm);

    Task<Result<UserConversationsVm>> GetUsersConversationsAsync();

    Task<Result<ConversationVm>> GetConversationDetailsAsync(int conversationId);

    Task<Result<MessageVm>> SendMessage(int conversationId, string messageContent);

    Task<bool> CanAccessConversation(int conversationId);
}