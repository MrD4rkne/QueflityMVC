#region

using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

#endregion

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
            Product = mapper.Map<ProductForCardVm>(purchasable),
            Email = await userRepository.GetEmailForUserAsync(userContext.UserId)
        };
        return Result<FirstMessageInConversationVm>.Success(firstMessageInConversationVm);
    }

    public async Task<Result> StartConversationAsync(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        if (!await userRepository.HasVerifiedEmail(userContext.UserId))
            return Result<FirstMessageInConversationVm>.Failure(Errors.User.EmailNotVerified);

        var productResult = await GetProductForDashboardVmAsync(firstMessageInConversationVm.Product.Id);
        if (productResult.IsFailure) return Result.Failure(productResult.Error);

        var email = await userRepository.GetEmailForUserAsync(userContext.UserId);
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure(Errors.User.EmailNotVerified);

        firstMessageInConversationVm = firstMessageInConversationVm with
        {
            Product = productResult.Value,
            Email = email
        };

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
        UserConversationsVm userConversationsVm = new()
        {
            PaginatedConversations = PaginationFactory.Default<ConversationShortVm>()
        };
        return GetUsersConversationsAsync(userConversationsVm);
    }

    public async Task<Result<UserConversationsVm>> GetUsersConversationsAsync(UserConversationsVm userConversationsVm)
    {
        var conversations = conversationRepository.GetUsersConversations(userContext.UserId);
        userConversationsVm.PaginatedConversations =
            await conversations.Paginate(userConversationsVm.PaginatedConversations,
                mapper.ConfigurationProvider);
        return Result<UserConversationsVm>.Success(userConversationsVm);
    }

    public Task<Result<UserConversationsVm>> GetAllConversationsAsync()
    {
        UserConversationsVm userConversationsVm = new()
        {
            PaginatedConversations = PaginationFactory.Default<ConversationShortVm>()
        };
        return GetAllConversationsAsync(userConversationsVm);
    }

    public async Task<Result<UserConversationsVm>> GetAllConversationsAsync(UserConversationsVm userConversationsVm)
    {
        var conversations = conversationRepository.GetAll();
        userConversationsVm.PaginatedConversations =
            await conversations.Paginate(userConversationsVm.PaginatedConversations,
                mapper.ConfigurationProvider);
        return Result<UserConversationsVm>.Success(userConversationsVm);
    }

    public async Task<Result<ConversationVm>> GetConversationDetailsAsync(int conversationId)
    {
        if (!await CanAccessConversation(conversationId))
            return Result<ConversationVm>.Failure(Errors.Conversation.DoesNotBelongToUser);

        var conversation = await conversationRepository.GetConversationDetails(conversationId);
        var conversationVm = mapper.Map<ConversationVm>(conversation);

        var messages = conversationRepository.GetMessagesForConversation(conversationId);
        conversationVm.Messages = await messages.ProjectTo<MessageVm>(mapper.ConfigurationProvider)
            .ToListAsync();

        return Result<ConversationVm>.Success(conversationVm);
    }

    public async Task<Result<MessageVm>> SendMessage(int conversationId, string messageContent)
    {
        if (!await CanAccessConversation(conversationId))
            return Result<MessageVm>.Failure(Errors.Conversation.DoesNotBelongToUser);

        Message message = new()
        {
            SentAt = DateTime.Now,
            UserId = userContext.UserId,
            Content = messageContent,
            ConversationId = conversationId
        };
        message = await conversationRepository.AddMessageAsync(message);

        return Result<MessageVm>.Success(mapper.Map<MessageVm>(message));
    }

    public async Task<Result<ProductForCardVm>> GetProductForDashboardVmAsync(int productId)
    {
        var product = await purchasableRepository.GetByIdAsync(productId);
        if (product is null) return Result<ProductForCardVm>.Failure(Errors.Product.DoesNotExist);

        return Result<ProductForCardVm>.Success(mapper.Map<ProductForCardVm>(product));
    }

    public async Task<bool> CanAccessConversation(int conversationId)
    {
        var conversation = await conversationRepository.GetByIdAsync(conversationId);
        if (conversation.UserId == userContext.UserId) return true;

        return await CanRespondToConversations(userContext.UserId);
    }

    private Task<bool> CanRespondToConversations(Guid userContextUserId)
    {
        return userRepository.HasClaimAsync(userContextUserId, Claims.CONVERSATIONS_RESPOND,
            Claims.CONVERSATIONS_RESPOND);
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