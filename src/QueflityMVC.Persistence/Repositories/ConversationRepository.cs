using Microsoft.EntityFrameworkCore;
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
            .AsNoTracking()
            .Include(convo => convo.Product)
            .Include(convo => convo.User)
            .Include(convo => convo.Messages
                .OrderBy(msg=>msg.SentAt)
                .TakeLast(20))
            .Where(c => c.UserId == userId);
    }
}