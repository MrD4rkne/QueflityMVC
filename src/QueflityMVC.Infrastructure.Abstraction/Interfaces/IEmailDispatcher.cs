namespace QueflityMVC.Infrastructure.Abstraction.Purchasables;

public interface IEmailDispatcher
{
    public Task SendEmailAsync(string recipient, string subject, string body);
}