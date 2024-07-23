using QueflityMVC.Infrastructure.Abstraction.Model;

namespace QueflityMVC.Infrastructure.Abstraction.Interfaces;

public interface IEmailDispatcher
{
    public Task SendEmailAsync(Mail mail);
}