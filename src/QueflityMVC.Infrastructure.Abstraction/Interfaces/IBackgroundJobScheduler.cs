using QueflityMVC.Infrastructure.Abstraction.Model;

namespace QueflityMVC.Infrastructure.Abstraction.Interfaces;

public interface IBackgroundJobScheduler
{
    public Task ScheduleSendMessageJob(Mail mail);
}