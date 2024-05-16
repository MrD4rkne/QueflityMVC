using System.Text;
using AutoMapper;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Purchasable;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;

namespace QueflityMVC.Application.Services;

public class MessageService : IMessageService
{
    private readonly IMapper _mapper;
    private readonly IPurchasableRepository _purchasableRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBackgroundJobScheduler _backgroundJobScheduler;

    public MessageService(IPurchasableRepository purchasableRepository, IUserRepository userRepository,
        IBackgroundJobScheduler backgroundJobScheduler, IMapper mapper)
    {
        _purchasableRepository = purchasableRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _backgroundJobScheduler = backgroundJobScheduler;
    }

    public async Task<Result<MessageVm>> GetContactVmAsync(int id, string userId)
    {
        if (!await _userRepository.HasVerifiedEmail(userId))
            return Result<MessageVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await _purchasableRepository.GetByIdAsync(id);
        if (purchasable is null) return Result<MessageVm>.Failure(Errors.Purchasable.DoesNotExist);

        MessageVm messageVm = new()
        {
            Purchasable = _mapper.Map<PurchasableForDashboardVm>(purchasable),
            Email = await _userRepository.GetEmailForUserAsync(userId)
        };
        return Result<MessageVm>.Success(messageVm);
    }

    public async Task<Result> SendMessageAsync(MessageVm messageVm, string userId)
    {
        if (!await _userRepository.HasVerifiedEmail(userId))
            return Result<MessageVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await _purchasableRepository.GetByIdAsync(messageVm.Purchasable.Id);
        if (purchasable is null) return Result<MessageVm>.Failure(Errors.Purchasable.DoesNotExist);

        messageVm = messageVm with { Purchasable = _mapper.Map<PurchasableForDashboardVm>(purchasable) };
        
        var email = await _userRepository.GetEmailForUserAsync(userId);
        if(string.IsNullOrWhiteSpace(email))
            return Result.Failure(Errors.User.EmailNotVerified);
        
        var message = BuildEmailBody(messageVm);
        var subject = $"COPY: Your message about {messageVm.Purchasable.Name} on {DateTime.Now}";
        await _backgroundJobScheduler.ScheduleSendMessageJob(new Mail
        {
            Body = message,
            Recipient = email,
            Subject = subject
        });
        return Result.Success();
    }


    private static string BuildEmailBody(MessageVm messageVm)
    {
        StringBuilder sb = new();
        sb.AppendLine($"This is a copy of the message you sent about {messageVm.Purchasable.Name} on {DateTime.Now}");
        sb.AppendLine();
        sb.AppendLine(messageVm.Message);
        sb.AppendLine(
            "Please do not reply to this email. If you have any questions, please contact us at our website.");
        return sb.ToString();
    }
}