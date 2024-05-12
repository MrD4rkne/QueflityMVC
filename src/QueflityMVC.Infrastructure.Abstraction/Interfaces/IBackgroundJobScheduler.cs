using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Abstraction.Purchasables;

public interface IBackgroundJobScheduler
{
    public Task ScheduleEmailJob(Message message);
}