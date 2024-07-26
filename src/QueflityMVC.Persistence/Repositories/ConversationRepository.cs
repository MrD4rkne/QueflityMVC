using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ConversationRepository(Context dbContext)
    : BaseRepository<Conversation>(dbContext), IConversationRepository
{
    public IQueryable<Conversation> GetUsersConversations(string userId, int lastMessageCount=20)
    {
        return DbContext.Conversations
            .AsNoTracking()
            .Include(convo => convo.Product)
            .Include(convo => convo.User)
            .Include(convo => convo.Messages
                .OrderByDescending(msg=>msg.SentAt)
                .Take(lastMessageCount)
                .OrderBy(msg=>msg.SentAt))
            .Where(c => c.UserId == userId);
    }
}