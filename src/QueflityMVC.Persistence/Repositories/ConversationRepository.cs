using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ConversationRepository(Context dbContext)
    : BaseRepository<Conversation>(dbContext), IConversationRepository
{
    public IQueryable<Conversation> GetUsersConversations(Guid userId, int lastMessageCount = 20)
    {
        return DbContext.Conversations
            .AsNoTracking()
            .Include(convo => convo.Messages
                .OrderByDescending(msg => msg.SentAt)
                .Take(lastMessageCount)
                .OrderBy(msg => msg.SentAt))
            .Include(convo => convo.Product)
            .ThenInclude(prod => (prod as Kit).Elements)
            .Where(c => c.UserId == userId);
    }

    public Task<Conversation> GetConversationDetails(int conversationId)
    {
        return DbContext.Conversations
            .AsNoTracking()
            .Include(convo => convo.Product)
            .ThenInclude(prod => (prod as Kit).Elements)
            .FirstOrDefaultAsync(c => c.Id == conversationId);
    }

    public IQueryable<Message> GetMessagesForConversation(int conversationId)
    {
        return DbContext.Messages
            .AsNoTracking()
            .Where(m => m.ConversationId == conversationId)
            .OrderBy(m => m.SentAt);
    }

    public async Task<Message> AddMessageAsync(Message message)
    {
        DbContext.Messages.Add(message);
        await DbContext.SaveChangesAsync();
        return message;
    }
}