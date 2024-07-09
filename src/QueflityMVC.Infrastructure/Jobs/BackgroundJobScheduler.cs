using Quartz;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;
using QueflityMVC.Infrastructure.Jobs;

namespace QueflityMVC.Infrastructure.Purchasables;

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
        jobData.Put("Mail", mail);

        var scheduler = await _schedulerFactory.GetScheduler();
        await scheduler.TriggerJob(SendEmailJob.Key, jobData);
    }
}