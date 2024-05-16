using Microsoft.Extensions.Logging;
using Quartz;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;

namespace QueflityMVC.Infrastructure.Jobs;

public class SendEmailJob : IJob
{
    public static readonly JobKey Key = new("send-copy-of-message", "email");

    private readonly IEmailDispatcher _emailDispatcher;
    private readonly ILogger<SendEmailJob> _logger;
    private readonly IBackgroundJobScheduler _backgroundJobScheduler;

    public SendEmailJob(ILogger<SendEmailJob> logger, IBackgroundJobScheduler backgroundJobScheduler ,IEmailDispatcher emailDispatcher)
    {
        _logger = logger;
        _backgroundJobScheduler = backgroundJobScheduler;
        _emailDispatcher = emailDispatcher;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        if(!TryParseMessage(context, out Mail? message))
        {
            _logger.LogError("Message not found in job data map: @{jobDetail}", context.JobDetail);
            return Task.CompletedTask;
        }
        
        try
        {
            return _emailDispatcher.SendEmailAsync(message!);
        }catch(Exception ex)
        {
            _logger.LogError(ex, "Error when sending email");
            _backgroundJobScheduler.ScheduleSendMessageJob(message!);
            return Task.FromException(ex);
        }
    }
    
    private bool TryParseMessage(IJobExecutionContext context, out Mail? message)
    {
        var dataMap = context.Trigger.JobDataMap;
        if(dataMap.TryGetValue("Mail", out var value))
        {
            if(value is Mail messageFromDataMap)
            {
                message = messageFromDataMap;
                return true;
            }
        }
        message = null;
        return false;
    }
}