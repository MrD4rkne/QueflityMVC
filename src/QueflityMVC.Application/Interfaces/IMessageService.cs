using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Interfaces;

public interface IMessageService
{
    Task<Result<MessageVm>> GetContactVmAsync(int id, string email);

    Task<Result> SendMessageAsync(MessageVm messageVm, string userId);
}