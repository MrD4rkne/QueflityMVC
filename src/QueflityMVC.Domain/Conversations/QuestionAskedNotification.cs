using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Conversations;

public class QuestionAskedNotification : Notification
{
    public Conversation Conversation { get; set; }

    public Message Message { get; set; }
}