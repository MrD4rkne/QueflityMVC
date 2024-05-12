using Quartz;
using QueflityMVC.Domain.Models;
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

    public async Task ScheduleEmailJob(Message message)
    {
        var jobData = new JobDataMap();
        jobData["Recipient"] = message.Recipient;
        jobData["Subject"] = message.Subject;
        jobData["Body"] = message.Body;
        var scheduler = await _schedulerFactory.GetScheduler();

        var job = JobBuilder.Create<SendEmailJob>()
            .WithIdentity(SendEmailJob.Key)
            .UsingJobData(jobData)
            .Build();

        var trigger = TriggerBuilder.Create()
            .WithIdentity("emailTrigger", "group1")
            .StartNow()
            .Build();
        await scheduler.ScheduleJob(job, trigger);
    }
}