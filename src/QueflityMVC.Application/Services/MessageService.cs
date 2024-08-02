using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Model;

namespace QueflityMVC.Application.Services;

public class MessageService(
    IProductRepository purchasableRepository,
    IUserRepository userRepository,
    IMessageRepository messageRepository,
    IConversationRepository conversationRepository,
    IBackgroundJobScheduler backgroundJobScheduler,
    IMapper mapper,
    IUserContext userContext)
    : IMessageService
{
    public async Task<Result<FirstMessageInConversationVm>> GetContactVmAsync(int purchasableId)
    {
        if (!await userRepository.HasVerifiedEmail(userContext.UserId))
            return Result<FirstMessageInConversationVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await purchasableRepository.GetByIdAsync(purchasableId);
        if (purchasable is null) return Result<FirstMessageInConversationVm>.Failure(Errors.Product.DoesNotExist);

        FirstMessageInConversationVm firstMessageInConversationVm = new()
        {
            Product = mapper.Map<ProductForDashboardVm>(purchasable),
            Email = await userRepository.GetEmailForUserAsync(userContext.UserId)
        };
        return Result<FirstMessageInConversationVm>.Success(firstMessageInConversationVm);
    }

    public async Task<Result> StartConversationAsync(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        if (!await userRepository.HasVerifiedEmail(userContext.UserId))
            return Result<FirstMessageInConversationVm>.Failure(Errors.User.EmailNotVerified);

        var purchasable = await purchasableRepository.GetByIdAsync(firstMessageInConversationVm.Product.Id);
        if (purchasable is null) return Result<FirstMessageInConversationVm>.Failure(Errors.Product.DoesNotExist);

        firstMessageInConversationVm = firstMessageInConversationVm with
        {
            Product = mapper.Map<ProductForDashboardVm>(purchasable)
        };

        var email = await userRepository.GetEmailForUserAsync(userContext.UserId);
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure(Errors.User.EmailNotVerified);

        firstMessageInConversationVm = firstMessageInConversationVm with { Email = email };

        Message message = new()
        {
            SentAt = DateTime.Now,
            UserId = userContext.UserId,
            Content = firstMessageInConversationVm.Message
        };

        Conversation conversation = new()
        {
            ProductId = firstMessageInConversationVm.Product.Id,
            UserId = userContext.UserId,
            Title = firstMessageInConversationVm.Title,
            IsClosed = false,
            Messages = [message]
        };
        _ = await conversationRepository.AddAsync(conversation);
        SentCopyEmail(firstMessageInConversationVm);

        return Result.Success();
    }

    public Task<Result<UserConversationsVm>> GetUsersConversationsAsync()
    {
        var conversations = conversationRepository.GetUsersConversations(userContext.UserId);
        var userConversionsVm = new UserConversationsVm
        {
            Conversations = conversations.ProjectTo<ConversationShortVm>(mapper.ConfigurationProvider).ToList()
        };
        return Task.FromResult(Result<UserConversationsVm>.Success(userConversionsVm));
    }

    public async Task<Result<ConversationVm>> GetConversationDetailsAsync(int conversationId)
    {
        var conversation = await conversationRepository.GetConversationDetails(conversationId);
        if (conversation is null) return Result<ConversationVm>.Failure(Errors.Conversation.DoesNotExist);
        if (conversation.UserId != userContext.UserId)
            return Result<ConversationVm>.Failure(Errors.Conversation.DoesNotBelongToUser);

        var conversationVm = mapper.Map<ConversationVm>(conversation);
        conversationVm.Messages = PaginationFactory.Default<MessageVm>();

        var messages = conversationRepository.GetMessagesForConversation(conversationId);
        conversationVm.Messages = await messages.Paginate(conversationVm.Messages, mapper.ConfigurationProvider);
        return Result<ConversationVm>.Success(conversationVm);
    }

    private void SentCopyEmail(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        var mailBody = BuildEmailBody(firstMessageInConversationVm);
        var subject = $"COPY: Your message about {firstMessageInConversationVm.Product.Name} on {DateTime.Now}";
        backgroundJobScheduler.ScheduleSendMessageJob(new Mail
        {
            Body = mailBody,
            RecipientName = firstMessageInConversationVm.Email,
            RecipientEmail = firstMessageInConversationVm.Email,
            Subject = subject
        });
    }

    private static string BuildEmailBody(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        StringBuilder sb = new();
        sb.AppendLine(
            $"This is a copy of the message you sent about {firstMessageInConversationVm.Product.Name} on {DateTime.Now}");
        sb.AppendLine();
        sb.AppendLine(firstMessageInConversationVm.Message);
        sb.AppendLine(
            "Please do not reply to this email. If you have any questions, please contact us at our website.");
        return sb.ToString();
    }
}