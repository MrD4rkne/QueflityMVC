using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IConversationRepository : IBaseRepository<Conversation>
{
    IQueryable<Conversation> GetUsersConversations(string userId, int lastMessageCount = 20);
    
    Task<Conversation> GetConversationDetails(int conversationId);
    
    IQueryable<Message> GetMessagesForConversation(int conversationId);
}