using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Message : BaseEntity
{
    public string Content { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }

    public DateTime SentAt { get; set; }

    public int ConversationId { get; set; }

    public Conversation Conversation { get; set; }
}