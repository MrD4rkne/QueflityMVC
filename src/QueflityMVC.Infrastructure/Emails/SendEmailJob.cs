using Microsoft.Extensions.Logging;
using Quartz;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Model;

namespace QueflityMVC.Infrastructure.Emails;

public class SendEmailJob(
    ILogger<SendEmailJob> logger,
    IBackgroundJobScheduler backgroundJobScheduler,
    IEmailDispatcher emailDispatcher)
    : IJob
{
    public const string DATA_KEY = "Mail";
    public static readonly JobKey Key = new("send-copy-of-message", "email");

    private readonly IBackgroundJobScheduler _backgroundJobScheduler = backgroundJobScheduler;

    public Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        if (!TryParseMessage(context, out var mail))
        {
            logger.LogError("Message not found in job data map: @{jobDetail}", context.JobDetail);
            return Task.CompletedTask;
        }

        try
        {
            return emailDispatcher.SendEmailAsync(mail!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when sending email");
            throw new JobExecutionException(ex, false);
        }
    }

    private bool TryParseMessage(IJobExecutionContext context, out Mail? mail)
    {
        var dataMap = context.Trigger.JobDataMap;
        if (dataMap.TryGetValue(DATA_KEY, out var value))
            if (value is Mail mailFromDataMap)
            {
                mail = mailFromDataMap;
                return true;
            }

        mail = null;
        return false;
    }
}