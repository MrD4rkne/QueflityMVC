using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Conversations;
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
            .ThenInclude(product => (product as Kit).Elements)
            .Include(convo => convo.Product)
            .ThenInclude(product => product.Image)
            .Where(c => c.UserId == userId);
    }

    public async Task<Conversation> GetConversationDetails(int conversationId)
    {
        var conversation = await DbContext.Conversations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == conversationId);
        conversation.Product = await DbContext.Set<Product>()
            .AsNoTracking()
            .Include(p => p.Image)
            .Include(product => (product as Kit).Elements)
            .FirstOrDefaultAsync(p => p.Id == conversation.ProductId);
        return conversation;
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

    public IQueryable<Conversation> GetAllConversations(int lastMessageCount = 20)
    {
        return DbContext.Conversations
            .AsNoTracking()
            .Include(convo => convo.Messages
                .OrderByDescending(msg => msg.SentAt)
                .Take(lastMessageCount)
                .OrderBy(msg => msg.SentAt))
            .Include(convo => convo.Product)
            .ThenInclude(product => (product as Kit).Elements)
            .Include(convo => convo.Product)
            .ThenInclude(product => product.Image);
    }

    public Task<Conversation> GetConversationByProductAndUserAsync(int purchasableId, Guid userId)
    {
        return DbContext.Conversations
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.ProductId == purchasableId && c.UserId == userId);
    }
}