using Quartz;
using QueflityMVC.Application.Notifications;
using QueflityMVC.Domain.Conversations;
using QueflityMVC.Infrastructure.Emails;

namespace QueflityMVC.Infrastructure.Jobs;

public class EmailNotificationService(
    ISchedulerFactory schedulerFactory,
    QuestionAskedEmailTemplate questionAskedEmailTemplate) : INotificationsService
{
    public async Task NotifyOfQuestionAskedAsync(QuestionAskedNotification questionAskedNotification)
    {
        var mail = await questionAskedEmailTemplate.BuildEmail(questionAskedNotification,
            questionAskedNotification.User.Email, questionAskedNotification.User.UserName);
        JobDataMap jobData = new();
        jobData.Put(SendEmailJob.DATA_KEY, mail);

        var scheduler = await schedulerFactory.GetScheduler();
        await scheduler.TriggerJob(SendEmailJob.Key, jobData);
    }
}