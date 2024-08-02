using Quartz;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Emails;

namespace QueflityMVC.Infrastructure.Jobs;

public class BackgroundJobScheduler : IBackgroundJobScheduler
{
    private readonly IEmailDispatcher _emailDispatcher;
    private readonly ISchedulerFactory _schedulerFactory;

    public BackgroundJobScheduler(IEmailDispatcher emailDispatcher, ISchedulerFactory schedulerFactory)
    {
        _emailDispatcher = emailDispatcher;
        _schedulerFactory = schedulerFactory;
    }

    public async Task ScheduleSendMessageJob(Mail mail)
    {
        JobDataMap jobData = new();
        jobData.Put(SendEmailJob.DATA_KEY, mail);

        var scheduler = await _schedulerFactory.GetScheduler();
        await scheduler.TriggerJob(SendEmailJob.Key, jobData);
    }
}