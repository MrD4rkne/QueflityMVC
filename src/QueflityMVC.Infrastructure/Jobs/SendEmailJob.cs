using Quartz;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;

namespace QueflityMVC.Infrastructure.Jobs;

public class SendEmailJob : IJob
{
    public static readonly JobKey Key = new("send-copy-of-message", "email");

    private readonly IEmailDispatcher _emailDispatcher;

    public SendEmailJob(IEmailDispatcher emailDispatcher)
    {
        _emailDispatcher = emailDispatcher;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var dataMap = context.JobDetail.JobDataMap;
        var recipient = dataMap.GetString("Recipient");
        var subject = dataMap.GetString("Subject");
        var body = dataMap.GetString("Body");
        return _emailDispatcher.SendEmailAsync(recipient, subject, body);
    }
}