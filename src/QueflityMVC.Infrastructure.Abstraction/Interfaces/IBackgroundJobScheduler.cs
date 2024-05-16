using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Abstraction.Purchasables;

public interface IBackgroundJobScheduler
{
    public Task ScheduleSendMessageJob(Mail mail);
}