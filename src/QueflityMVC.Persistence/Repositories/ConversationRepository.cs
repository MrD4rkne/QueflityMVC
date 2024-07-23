using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ConversationRepository(Context dbContext)
    : BaseRepository<Conversation>(dbContext), IConversationRepository
{
    public IQueryable<Conversation> GetUsersConversations(string userId)
    {
        return dbContext.Conversations
            .Where(c => c.UserId == userId);
    }
}