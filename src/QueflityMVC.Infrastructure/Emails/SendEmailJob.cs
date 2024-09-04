using Microsoft.Extensions.Logging;
using Quartz;
using QueflityMVC.Application.Emails;

namespace QueflityMVC.Infrastructure.Emails;

public class SendEmailJob(
    ILogger<SendEmailJob> logger,
    IEmailSender emailSender)
    : IJob
{
    public const string DATA_KEY = "Mail";
    public static readonly JobKey Key = new("send-copy-of-message", "email");

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
            return emailSender.SendMailAsync(mail!);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error when sending email");
            throw new JobExecutionException(ex, false);
        }
    }

    private bool TryParseMessage(IJobExecutionContext context, out Email? mail)
    {
        var dataMap = context.Trigger.JobDataMap;
        if (dataMap.TryGetValue(DATA_KEY, out object value))
        {
            if (value is Email mailFromDataMap)
            {
                mail = mailFromDataMap;
                return true;
            }
        }

        mail = null;
        return false;
    }
}