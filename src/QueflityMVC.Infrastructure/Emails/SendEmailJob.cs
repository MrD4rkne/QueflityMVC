using Microsoft.Extensions.Logging;
using Quartz;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

namespace QueflityMVC.Infrastructure.Emails;

public class SendEmailJob(
    ILogger<SendEmailJob> logger,
    IBackgroundJobScheduler backgroundJobScheduler,
    IEmailDispatcher emailDispatcher)
    : IJob
{
    public static readonly JobKey Key = new("send-copy-of-message", "email");

    private readonly IBackgroundJobScheduler _backgroundJobScheduler = backgroundJobScheduler;

    public Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        if (!TryParseMessage(context, out var message))
        {
            logger.LogError("Message not found in job data map: @{jobDetail}", context.JobDetail);
            return Task.CompletedTask;
        }

        try
        {
            return emailDispatcher.SendEmailAsync(message!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when sending email");
            throw new JobExecutionException(ex, false);
        }
    }

    private bool TryParseMessage(IJobExecutionContext context, out Mail? message)
    {
        var dataMap = context.Trigger.JobDataMap;
        if (dataMap.TryGetValue("Mail", out var value))
            if (value is Mail messageFromDataMap)
            {
                message = messageFromDataMap;
                return true;
            }

        message = null;
        return false;
    }
}