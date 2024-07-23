using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Model;
using MessageVm = QueflityMVC.Application.ViewModels.Other.MessageVm;

namespace QueflityMVC.Application.Services;

public class MessageService(
    IProductRepository purchasableRepository,
    IUserRepository userRepository,
    IMessageRepository messageRepository,
    IConversationRepository conversationRepository,
    IBackgroundJobScheduler backgroundJobScheduler,
    IMapper mapper)
    : IMessageService
{
    private readonly IConversationRepository _conversationRepository = conversationRepository;

    public async Task<Result<MessageVm>> GetContactVmAsync(int id, string userId)
    {
        if (!await userRepository.HasVerifiedEmail(userId))
            return Result<MessageVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await purchasableRepository.GetByIdAsync(id);
        if (purchasable is null) return Result<MessageVm>.Failure(Errors.Product.DoesNotExist);

        MessageVm messageVm = new()
        {
            Product = mapper.Map<ProductForDashboardVm>(purchasable),
            Email = await userRepository.GetEmailForUserAsync(userId)
        };
        return Result<MessageVm>.Success(messageVm);
    }

    public async Task<Result> StartConversationAsync(MessageVm messageVm, string userId)
    {
        if (!await userRepository.HasVerifiedEmail(userId))
            return Result<MessageVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await purchasableRepository.GetByIdAsync(messageVm.Product.Id);
        if (purchasable is null) return Result<MessageVm>.Failure(Errors.Product.DoesNotExist);

        messageVm = messageVm with { Product = mapper.Map<ProductForDashboardVm>(purchasable) };

        var email = await userRepository.GetEmailForUserAsync(userId);
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure(Errors.User.EmailNotVerified);

        messageVm = messageVm with { Email = email };

        Message message = new()
        {
            SentAt = DateTime.Now,
            UserId = userId,
            Content = messageVm.Message
        };

        Conversation conversation = new()
        {
            ProductId = messageVm.Product.Id,
            UserId = userId,
            Title = messageVm.Title,
            IsClosed = false,
            Messages = [message]
        };
        _ = await _conversationRepository.AddAsync(conversation);
        SentCopyEmail(messageVm);

        return Result.Success();
    }

    public Task<Result<UserConversationsVm>> GetUsersConversationsAsync(string userId)
    {
        var conversations = _conversationRepository.GetUsersConversations(userId);
        var userConversionsVm = new UserConversationsVm
        {
            Conversations = conversations.ProjectTo<ConversationVm>(mapper.ConfigurationProvider).ToList()
        };
        return Task.FromResult(Result<UserConversationsVm>.Success(userConversionsVm));
    }

    private void SentCopyEmail(MessageVm messageVm)
    {
        var mailBody = BuildEmailBody(messageVm);
        var subject = $"COPY: Your message about {messageVm.Product.Name} on {DateTime.Now}";
        backgroundJobScheduler.ScheduleSendMessageJob(new Mail
        {
            Body = mailBody,
            RecipientName = messageVm.Email,
            RecipientEmail = messageVm.Email,
            Subject = subject
        });
    }

    private static string BuildEmailBody(MessageVm messageVm)
    {
        StringBuilder sb = new();
        sb.AppendLine($"This is a copy of the message you sent about {messageVm.Product.Name} on {DateTime.Now}");
        sb.AppendLine();
        sb.AppendLine(messageVm.Message);
        sb.AppendLine(
            "Please do not reply to this email. If you have any questions, please contact us at our website.");
        return sb.ToString();
    }
}