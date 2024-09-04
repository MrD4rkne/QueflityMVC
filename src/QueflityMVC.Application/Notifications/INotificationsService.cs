using QueflityMVC.Domain.Conversations;

namespace QueflityMVC.Application.Notifications;

public interface INotificationsService
{
    Task NotifyOfQuestionAskedAsync(QuestionAskedNotification questionAskedNotification);
}