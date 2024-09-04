using QueflityMVC.Domain.Conversations;

namespace QueflityMVC.Domain.Interfaces;

public interface IConversationRepository : IBaseRepository<Conversation>
{
    IQueryable<Conversation> GetUsersConversations(Guid userId, int lastMessageCount = 20);

    Task<Conversation> GetConversationDetails(int conversationId);

    IQueryable<Message> GetMessagesForConversation(int conversationId);

    Task<Message> AddMessageAsync(Message message);

    IQueryable<Conversation> GetAllConversations(int lastMessageCount = 20);

    Task<Conversation> GetConversationByProductAndUserAsync(int purchasableId, Guid userId);
}