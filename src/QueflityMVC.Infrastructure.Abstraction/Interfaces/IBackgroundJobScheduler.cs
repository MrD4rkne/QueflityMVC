using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Abstraction.Interfaces;

public interface IBackgroundJobScheduler
{
    public Task ScheduleSendMessageJob(Mail mail);
}