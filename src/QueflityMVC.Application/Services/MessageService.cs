using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Notifications;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Message;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Conversations;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.Services;

public class MessageService(
    IProductRepository purchasableRepository,
    IUserRepository userRepository,
    INotificationsService notificationsService,
    IConversationRepository conversationRepository,
    IMapper mapper,
    IUserContext userContext)
    : IMessageService
{
    public async Task<Result<FirstMessageInConversationVm>> GetContactVmAsync(int purchasableId)
    {
        if (!await userRepository.HasVerifiedEmail(userContext.UserId))
        {
            return Result<FirstMessageInConversationVm>.Failure(Errors.User.EmailNotVerified);
        }

        var existingConversation = await GetConversationIdByProductAsync(purchasableId, userContext.UserId);
        if (existingConversation.IsSuccess)
        {
            return Result<FirstMessageInConversationVm>.Failure(Errors.Conversation.AlreadyExists);
        }

        var purchasable = await purchasableRepository.GetByIdAsync(purchasableId);
        if (purchasable is null)
        {
            return Result<FirstMessageInConversationVm>.Failure(Errors.Product.DoesNotExist);
        }

        FirstMessageInConversationVm firstMessageInConversationVm = new()
        {
            Product = mapper.Map<ProductShortVm>(purchasable),
            Email = await userRepository.GetEmailForUserAsync(userContext.UserId)
        };
        return Result<FirstMessageInConversationVm>.Success(firstMessageInConversationVm);
    }

    public Task<Result<int>> GetConversationIdByProductAsync(int purchasableId)
    {
        return GetConversationIdByProductAsync(purchasableId, userContext.UserId);
    }

    public async Task<Result<int>> StartConversationAsync(FirstMessageInConversationVm firstMessageInConversationVm)
    {
        if (!await userRepository.HasVerifiedEmail(userContext.UserId))
        {
            return Result<int>.Failure(Errors.User.EmailNotVerified);
        }

        var productResult = await GetProductForContactVmAsync(firstMessageInConversationVm.Product.Id);
        if (productResult.IsFailure)
        {
            return Result<int>.Failure(productResult.Error);
        }

        string? email = await userRepository.GetEmailForUserAsync(userContext.UserId);
        if (string.IsNullOrWhiteSpace(email))
        {
            return Result<int>.Failure(Errors.User.EmailNotVerified);
        }

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

        var existingConversationId = await GetConversationIdByProductAsync(firstMessageInConversationVm.Product.Id);

        Conversation conversation = null;

        if (existingConversationId.IsSuccess)
        {
            message.ConversationId = existingConversationId.Value;
            _ = await conversationRepository.AddMessageAsync(message);
        }
        else
        {
            conversation = new Conversation
            {
                ProductId = firstMessageInConversationVm.Product.Id,
                UserId = userContext.UserId,
                Title = firstMessageInConversationVm.Title,
                IsClosed = false,
                Messages = [message]
            };

            _ = await conversationRepository.AddAsync(conversation);
        }

        await notificationsService.NotifyOfQuestionAskedAsync(new QuestionAskedNotification
        {
            Conversation = conversation,
            Message = message,
            User = await userRepository.GetUserByIdAsync(userContext.UserId)
        });

        return Result<int>.Success(conversation.Id);
    }

    public Task<Result<UserConversationsVm>> GetUsersConversationsAsync()
    {
        UserConversationsVm userConversationsVm = new();
        return GetUsersConversationsAsync(userConversationsVm);
    }

    public async Task<Result<UserConversationsVm>> GetUsersConversationsAsync(UserConversationsVm userConversationsVm)
    {
        var conversations = conversationRepository.GetUsersConversations(userContext.UserId);

        userConversationsVm = await GetConversationsPaginatedAsync(conversations, userConversationsVm);
        return Result<UserConversationsVm>.Success(userConversationsVm);
    }

    public Task<Result<UserConversationsVm>> GetAllButCurrentUserConversationsAsync()
    {
        UserConversationsVm userConversationsVm = new();
        return GetAllButCurrentUserConversationsAsync(userConversationsVm);
    }

    public async Task<Result<UserConversationsVm>> GetAllButCurrentUserConversationsAsync(
        UserConversationsVm userConversationsVm)
    {
        var conversations = conversationRepository.GetAllConversations();

        // If user is authenticated, filter out conversations that belong to the user
        if (userContext.IsAuthenticated)
        {
            conversations = conversations.Where(conv => conv.UserId != userContext.UserId);
        }

        userConversationsVm = await GetConversationsPaginatedAsync(conversations, userConversationsVm);
        return Result<UserConversationsVm>.Success(userConversationsVm);
    }

    public async Task<Result<ConversationVm>> GetConversationDetailsAsync(int conversationId)
    {
        if (!await CanAccessConversation(conversationId))
        {
            return Result<ConversationVm>.Failure(Errors.Conversation.DoesNotBelongToUser);
        }

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
        {
            return Result<MessageVm>.Failure(Errors.Conversation.DoesNotBelongToUser);
        }

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

    public async Task<Result<ProductShortVm>> GetProductForContactVmAsync(int productId)
    {
        var product = await purchasableRepository.GetByIdAsync(productId);
        if (product is null)
        {
            return Result<ProductShortVm>.Failure(Errors.Product.DoesNotExist);
        }

        return Result<ProductShortVm>.Success(mapper.Map<ProductShortVm>(product));
    }

    public async Task<bool> CanAccessConversation(int conversationId)
    {
        var conversation = await conversationRepository.GetByIdAsync(conversationId);
        if (conversation.UserId == userContext.UserId)
        {
            return true;
        }

        return await CanRespondToConversations(userContext.UserId);
    }

    private async Task<Result<int>> GetConversationIdByProductAsync(int purchasableId, Guid userId)
    {
        var conversation = await conversationRepository.GetConversationByProductAndUserAsync(purchasableId, userId);
        if (conversation is not null)
        {
            return Result<int>.Success(conversation.Id);
        }

        return Result<int>.Failure(Errors.Conversation.DoesNotExist);
    }

    private async Task<UserConversationsVm> GetConversationsPaginatedAsync(IQueryable<Conversation> conversations,
        UserConversationsVm userConversationsVm)
    {
        if (userConversationsVm.ShouldSortFromTheLatest)
        {
            conversations = conversations.OrderByDescending(convo => convo.Messages
                .Max(message => message.SentAt));
        }
        else
        {
            conversations = conversations.OrderBy(convo => convo.Messages
                .Max(message => message.SentAt));
        }

        userConversationsVm =
            await conversations.Paginate<Conversation, ConversationShortVm, UserConversationsVm>(userConversationsVm,
                mapper.ConfigurationProvider);
        return userConversationsVm;
    }

    private Task<bool> CanRespondToConversations(Guid userContextUserId)
    {
        return userRepository.HasClaimAsync(userContextUserId, Claims.CONVERSATIONS_RESPOND,
            Claims.CONVERSATIONS_RESPOND);
    }
}