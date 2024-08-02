using QueflityMVC.Domain.Models;

namespace QueflityMVC.Infrastructure.Abstraction.Interfaces;

public interface IEmailDispatcher
{
    public Task SendEmailAsync(Mail mail);
}