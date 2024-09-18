using AutoMapper;
using Moq;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Notifications;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.UnitTests.Common;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Conversations;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class MessageServiceTests
{
    private readonly Mock<IProductRepository> _productRepository;
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<INotificationsService> _notificationsService;
    private readonly Mock<IConversationRepository> _conversationRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUserContext> _userContext;
    private readonly IMessageService _messageService;
    
    private readonly Guid _userId = Guid.NewGuid();
        
    public MessageServiceTests()
    {
        _productRepository = new Mock<IProductRepository>();
        _userRepository = new Mock<IUserRepository>();
        _notificationsService = new Mock<INotificationsService>();
        _conversationRepository = new Mock<IConversationRepository>();
        _mapper = new Mock<IMapper>();
        
        _userContext = new Mock<IUserContext>();
        _userContext
            .Setup(context => context.UserId)
            .Returns(_userId);
        _userContext
            .Setup(context => context.IsAuthenticated)
            .Returns(true);
        
        _messageService = new MessageService(
            _productRepository.Object,
            _userRepository.Object,
            _notificationsService.Object,
            _conversationRepository.Object,
            _mapper.Object,
            _userContext.Object);
    }
    
    [Fact]
    public async Task Get_GetContactVmAsync_UserHasNotVerifiedEmail_ReturnsError()
    {
        // Arrange
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _messageService.GetContactVmAsync(1);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.User.EMAIL_NOT_VERIFIED);
    }
    
    [Fact]
    public async Task Get_GetContactVmAsync_ConversationAlreadyExists_ReturnsError()
    {
        // Arrange
        int productId = 1;
        Conversation conversation = new Conversation()
        {
            Id = 1,
            ProductId = productId,
            UserId = _userId,
            Messages = [],
            IsClosed = false,
            Title = "Title"
        };
        
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);
        
        _conversationRepository
            .Setup(repository => repository.GetConversationByProductAndUserAsync(productId, _userId))
            .ReturnsAsync(conversation);
        
        // Act
        var result = await _messageService.GetContactVmAsync(productId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.ALREADY_EXISTS);
    }
    
    [Fact]
    public async Task Get_GetContactVmAsync_ProductDoesNotExist_ReturnsError()
    {
        // Arrange
        int productId = 1;
        
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);
        
        _conversationRepository
            .Setup(repository => repository.GetConversationByProductAndUserAsync(productId, _userId))
            .ReturnsAsync((Conversation)null);
        
        _productRepository.Setup(repository => repository.GetByIdAsync(productId))
            .ReturnsAsync((Product)null);
        
        // Act
        var result = await _messageService.GetContactVmAsync(productId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Product.DOES_NOT_EXIST);
    }
    
    [Fact]
    public async Task Get_GetContactVmAsync_ReturnsSuccess()
    {
        // Arrange
        int productId = 1;
        Product product = new Item()
        {
            Id = productId,
            Name = "Name",
            ShouldBeShown = true,
            OrderNo = 1,
            CategoryId = 5,
            ImageId = 3,
            Image = new Image()
            {
                Id = 3,
                AltDescription = "AltDescription",
                FileUrl = "FileUrl"
            }
        };
        
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);
        
        _conversationRepository
            .Setup(repository => repository.GetConversationByProductAndUserAsync(productId, _userId))
            .ReturnsAsync((Conversation)null);
        
        _productRepository.Setup(repository => repository.GetByIdAsync(productId))
            .ReturnsAsync(product);
        
        string email = "email@queflity.mvc";
        _userRepository.Setup(repository => repository.GetEmailForUserAsync(_userId))
            .ReturnsAsync(email);
        
        _mapper.Setup(mapper=>mapper.Map<ProductShortVm>(It.IsAny<Product>()))
            .Returns((Product mappedProduct) => new ProductShortVm()
            {
                Id =mappedProduct.Id,
                Name = mappedProduct.Name,
                Image = new ImageVm()
                {
                    Id = mappedProduct.Image.Id,
                    AltDescription = mappedProduct.Image.AltDescription,
                    FileUrl = mappedProduct.Image.FileUrl
                }
            });
        
        // Act
        var result = await _messageService.GetContactVmAsync(productId);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeEquivalentTo(new FirstMessageInConversationVm()
        {
            Email = email,
            Product = new ProductShortVm()
            {
                Id = product.Id,
                Name = product.Name,
                Image = new ImageVm()
                {
                    Id = product.Image.Id,
                    AltDescription = product.Image.AltDescription,
                    FileUrl = product.Image.FileUrl
                }
            }
        });
        
    }
    
    [Fact]
    public async Task Get_GetConversationIdByProductAsync_ReturnsSuccess()
    {
        // Arrange
        int productId = 1;
        Product product = new Item()
        {
            Id = productId,
            Name = "Name",
            ShouldBeShown = true,
            OrderNo = 1,
            CategoryId = 5,
            ImageId = 3,
            Image = new Image()
            {
                Id = 3,
                AltDescription = "AltDescription",
                FileUrl = "FileUrl"
            }
        };
        
        Conversation conversation = new Conversation()
        {
            Id = 1,
            ProductId = productId,
            UserId = _userId,
            Messages = [],
            IsClosed = false,
            Title = "Title"
        };
        
        _productRepository.Setup(repository => repository.GetByIdAsync(productId))
            .ReturnsAsync(product);
        
        _conversationRepository.Setup(repository => repository.GetConversationByProductAndUserAsync(productId, _userId))
            .ReturnsAsync(conversation);
        
        // Act
        var result = await _messageService.GetConversationIdByProductAsync(productId);
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        _conversationRepository.Verify(repository => repository.GetConversationByProductAndUserAsync(productId, _userId), Times.AtLeastOnce);
    }
    
    [Fact]
    public async Task Get_GetConversationIdByProductAsync_OnNonExisting_ReturnError()
    {
        // Arrange
        int productId = 1;
        Product product = new Item()
        {
            Id = productId,
            Name = "Name",
            ShouldBeShown = true,
            OrderNo = 1,
            CategoryId = 5,
            ImageId = 3,
            Image = new Image()
            {
                Id = 3,
                AltDescription = "AltDescription",
                FileUrl = "FileUrl"
            }
        };
        
        _productRepository.Setup(repository => repository.GetByIdAsync(productId))
            .ReturnsAsync(product);
        
        _conversationRepository.Setup(repository => repository.GetConversationByProductAndUserAsync(productId, _userId))
            .ReturnsAsync((Conversation)null);
        
        // Act
        var result = await _messageService.GetConversationIdByProductAsync(productId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.DOES_NOT_EXIST);
        _conversationRepository.Verify(repository => repository.GetConversationByProductAndUserAsync(productId, _userId), Times.AtLeastOnce);
    }
    
    [Fact]
    public async Task Create_StartConversationAsync_UserHasNotVerifiedEmail_ReturnsError()
    {
        // Arrange
        FirstMessageInConversationVm firstMessageInConversationVm = new FirstMessageInConversationVm()
        {
            Product = new ProductShortVm()
            {
                Id = 1,
                Name = "Name",
                Image = new ImageVm()
                {
                    Id = 3,
                    AltDescription = "AltDescription",
                    FileUrl = "FileUrl"
                }
            },
            Title = "Title",
            Message = "Message"
        };
        
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(false);
        
        // Act
        var result = await _messageService.StartConversationAsync(firstMessageInConversationVm);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.User.EMAIL_NOT_VERIFIED);
    }
    
    [Fact]
    public async Task Create_StartConversationAsync_ProductDoesNotExist_ReturnsError()
    {
        // Arrange
        FirstMessageInConversationVm firstMessageInConversationVm = new FirstMessageInConversationVm()
        {
            Product = new ProductShortVm()
            {
                Id = 1,
                Name = "Name",
                Image = new ImageVm()
                {
                    Id = 3,
                    AltDescription = "AltDescription",
                    FileUrl = "FileUrl"
                }
            },
            Title = "Title",
            Message = "Message"
        };
        
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);
        
        _productRepository.Setup(repository => repository.GetByIdAsync(firstMessageInConversationVm.Product.Id))
            .ReturnsAsync((Product)null);
        
        // Act
        var result = await _messageService.StartConversationAsync(firstMessageInConversationVm);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Product.DOES_NOT_EXIST);
    }

    [Fact]
    public async Task Create_StartConversationAsync_NonExistingConversation_ReturnsSuccess()
    {
        // Arrange
        FirstMessageInConversationVm firstMessageInConversationVm = new FirstMessageInConversationVm()
        {
            Product = new ProductShortVm()
            {
                Id = 1,
                Name = "Name",
                Image = new ImageVm()
                {
                    Id = 3,
                    AltDescription = "AltDescription",
                    FileUrl = "FileUrl"
                }
            },
            Title = "Title",
            Message = "Message"
        };

        Product product = new Item()
        {
            Id = firstMessageInConversationVm.Product.Id,
            Name = "Name",
            ShouldBeShown = true,
            OrderNo = 1,
            CategoryId = 5,
            ImageId = 3,
            Image = new Image()
            {
                Id = 3,
                AltDescription = "AltDescription",
                FileUrl = "FileUrl"
            }
        };

        string email = "email@queflity.mvc";
        _userRepository.Setup(repository => repository.GetEmailForUserAsync(_userId))
            .ReturnsAsync(email);
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);

        _productRepository.Setup(repository => repository.GetByIdAsync(firstMessageInConversationVm.Product.Id))
            .ReturnsAsync(product);
        
        _conversationRepository.Setup(repository => repository.GetConversationByProductAndUserAsync(firstMessageInConversationVm.Product.Id, _userId))
            .ReturnsAsync((Conversation)null);
        
        _mapper.Setup(mapper=>mapper.Map<ProductShortVm>(It.IsAny<Product>()))
            .Returns((Product mappedProduct) => new ProductShortVm()
            {
                Id =mappedProduct.Id,
                Name = mappedProduct.Name,
                Image = new ImageVm()
                {
                    Id = mappedProduct.Image.Id,
                    AltDescription = mappedProduct.Image.AltDescription,
                    FileUrl = mappedProduct.Image.FileUrl
                }
            });

        int conversationId = 10;
        _conversationRepository.Setup(repository => repository.AddAsync(It.IsAny<Conversation>()))
            .Callback((Conversation conversation) =>
            {
                conversation.Id = conversationId;
            });

        // Act
        var result = await _messageService.StartConversationAsync(firstMessageInConversationVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(conversationId);
        
        _conversationRepository.Verify(repository => repository.AddAsync(It.Is<Conversation>(conv=>
            conv.ProductId == firstMessageInConversationVm.Product.Id &&
            conv.UserId == _userId &&
            conv.Title == firstMessageInConversationVm.Title &&
            conv.Messages.Count == 1 &&
            conv.Messages[0].Content == firstMessageInConversationVm.Message &&
            conv.UserId == _userId &&
            conv.IsClosed == false
            )), Times.Once);
        _notificationsService.Verify(service => service.NotifyOfQuestionAskedAsync(It.Is<QuestionAskedNotification>(notification=>
            notification.Conversation.Id==conversationId &&
            notification.Conversation.ProductId == firstMessageInConversationVm.Product.Id &&
            notification.Conversation.UserId == _userId &&
            notification.Conversation.Title == firstMessageInConversationVm.Title &&
            notification.Conversation.UserId == _userId &&
            notification.Message.Content == firstMessageInConversationVm.Message &&
            notification.Message.UserId == _userId
            )), Times.Once);
    }
    
    [Fact]
    public async Task Create_StartConversationAsync_ExistingConversation_ReturnsSuccess()
    {
        // Arrange
        FirstMessageInConversationVm firstMessageInConversationVm = new FirstMessageInConversationVm()
        {
            Product = new ProductShortVm()
            {
                Id = 1,
                Name = "Name",
                Image = new ImageVm()
                {
                    Id = 3,
                    AltDescription = "AltDescription",
                    FileUrl = "FileUrl"
                }
            },
            Title = "Title",
            Message = "Message"
        };

        Product product = new Item()
        {
            Id = firstMessageInConversationVm.Product.Id,
            Name = "Name",
            ShouldBeShown = true,
            OrderNo = 1,
            CategoryId = 5,
            ImageId = 3,
            Image = new Image()
            {
                Id = 3,
                AltDescription = "AltDescription",
                FileUrl = "FileUrl"
            }
        };

        string email = "email@queflity.mvc";
        _userRepository.Setup(repository => repository.GetEmailForUserAsync(_userId))
            .ReturnsAsync(email);
        _userRepository
            .Setup(repository => repository.HasVerifiedEmail(_userId))
            .ReturnsAsync(true);

        _productRepository.Setup(repository => repository.GetByIdAsync(firstMessageInConversationVm.Product.Id))
            .ReturnsAsync(product);
        
        int conversationId = 10;
        Conversation conversation = new Conversation()
        {
            Id = conversationId,
            ProductId = firstMessageInConversationVm.Product.Id,
            UserId = _userId,
            Messages = new List<Message>()
            {
                new Message()
                {
                    Content = "Content",
                    SentAt = DateTime.Now,
                    UserId = _userId
                }
            },
            IsClosed = false,
            Title = "ConvoTitle"
        };
        
        _conversationRepository.Setup(repository => repository.GetConversationByProductAndUserAsync(firstMessageInConversationVm.Product.Id, _userId))
            .ReturnsAsync(conversation);
        
        _mapper.Setup(mapper=>mapper.Map<ProductShortVm>(It.IsAny<Product>()))
            .Returns((Product mappedProduct) => new ProductShortVm()
            {
                Id =mappedProduct.Id,
                Name = mappedProduct.Name,
                Image = new ImageVm()
                {
                    Id = mappedProduct.Image.Id,
                    AltDescription = mappedProduct.Image.AltDescription,
                    FileUrl = mappedProduct.Image.FileUrl
                }
            });

        // Act
        var result = await _messageService.StartConversationAsync(firstMessageInConversationVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBe(conversationId);
        
        _conversationRepository.Verify(repository => repository.AddAsync(It.IsAny<Conversation>()), Times.Never());
        _conversationRepository.Verify(repository=>repository.AddMessageAsync(It.Is<Message>(msg=>
            msg.ConversationId == conversationId &&
            msg.Content == firstMessageInConversationVm.Message &&
            msg.UserId == _userId
            )), Times.Once);
        
        _notificationsService.Verify(service => service.NotifyOfQuestionAskedAsync(It.Is<QuestionAskedNotification>(notification=>
            notification.Conversation.Id==conversationId &&
            notification.Conversation.ProductId == firstMessageInConversationVm.Product.Id &&
            notification.Conversation.UserId == _userId &&
            notification.Conversation.Title == conversation.Title &&
            notification.Conversation.UserId == _userId &&
            notification.Message.Content == firstMessageInConversationVm.Message &&
            notification.Message.UserId == _userId
            )), Times.Once);
    }
    
    [Fact]
    public async Task Get_GetConversationDetailsAsync_CannotAccessConversation_ReturnsError()
    {
        // Arrange
        int conversationId = 1;
        
        Guid otherUserId = _userId.GetDifferentGuid();
        Conversation conversation= new Conversation()
        {
            Id = conversationId,
            ProductId = 1,
            UserId = otherUserId,
            Messages = new List<Message>()
            {
                new Message()
                {
                    Content = "Content",
                    SentAt = DateTime.Now,
                    UserId = otherUserId
                }
            },
            IsClosed = false,
            Title = "ConvoTitle"
        };
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync(conversation);
        _userRepository.Setup(userRepo=>userRepo.HasClaimAsync(_userId, Claims.CONVERSATIONS_RESPOND, Claims.CONVERSATIONS_RESPOND))
            .ReturnsAsync(false);
        
        // Act
        var result = await _messageService.GetConversationDetailsAsync(conversationId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.DOES_NOT_EXIST);
    }
    
    [Fact]
    public async Task Get_GetConversationDetailsAsync_ConversationDoesNotExist_ReturnsError()
    {
        // Arrange
        int conversationId = 1;
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync((Conversation)null);
        
        // Act
        var result = await _messageService.GetConversationDetailsAsync(conversationId);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.DOES_NOT_EXIST);
    }

    public async Task SendMessage_OnCannotAccessConversation_ReturnsError()
    {
        // Arrange
        int conversationId = 1;
        
        Guid otherUserId = _userId.GetDifferentGuid();
        Conversation conversation= new Conversation()
        {
            Id = conversationId,
            ProductId = 1,
            UserId = otherUserId,
            Messages =
            [
                new Message()
                {
                    Content = "Content",
                    SentAt = DateTime.Now,
                    UserId = otherUserId
                }
            ],
            IsClosed = false,
            Title = "ConvoTitle"
        };
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync(conversation);
        _userRepository.Setup(userRepo=>userRepo.HasClaimAsync(_userId, Claims.CONVERSATIONS_RESPOND, Claims.CONVERSATIONS_RESPOND))
            .ReturnsAsync(false);
        
        // Act
        var result = await _messageService.SendMessage(conversationId, "Message");
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.DOES_NOT_EXIST);
        
        _conversationRepository.Verify(conversationRepository=> conversationRepository.AddMessageAsync(It.IsAny<Message>()), Times.Never());
    }
    
    [Fact]
    public async Task SendMessage_OnConversationDoesNotExist_ReturnsError()
    {
        // Arrange
        int conversationId = 1;
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync((Conversation)null);
        
        // Act
        var result = await _messageService.SendMessage(conversationId, "Message");
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Code.ShouldBe(ErrorCodes.Conversation.DOES_NOT_EXIST);
        
        _conversationRepository.Verify(conversationRepository=> conversationRepository.AddMessageAsync(It.IsAny<Message>()), Times.Never());
    }
    
    [Fact]
    public async Task SendMessage_OwnsConversation_ReturnsSuccess()
    {
        // Arrange
        int conversationId = 1;
        
        Conversation conversation= new Conversation()
        {
            Id = conversationId,
            ProductId = 1,
            UserId = _userId,
            Messages =
            [
                new Message()
                {
                    Content = "Content",
                    SentAt = DateTime.Now,
                    UserId = _userId
                }
            ],
            IsClosed = false,
            Title = "ConvoTitle"
        };
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync(conversation);
        _userRepository.Setup(userRepo=>userRepo.HasClaimAsync(_userId, Claims.CONVERSATIONS_RESPOND, Claims.CONVERSATIONS_RESPOND))
            .ReturnsAsync(false);
        
        // Act
        var result = await _messageService.SendMessage(conversationId, "Message");
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _conversationRepository.Verify(conversationRepository=> conversationRepository.AddMessageAsync(It.Is<Message>(msg=>
            msg.ConversationId == conversationId &&
            msg.Content == "Message" &&
            msg.UserId == _userId
            )), Times.Once());
    }
    
    [Fact]
    public async Task SendMessage_HasClaimsToAnswerConversations_ReturnsSuccess()
    {
        // Arrange
        int conversationId = 1;
        
        Guid otherUserId = _userId.GetDifferentGuid();
        Conversation conversation= new Conversation()
        {
            Id = conversationId,
            ProductId = 1,
            UserId = otherUserId,
            Messages =
            [
                new Message()
                {
                    Content = "Content",
                    SentAt = DateTime.Now,
                    UserId = otherUserId
                }
            ],
            IsClosed = false,
            Title = "ConvoTitle"
        };
        
        _conversationRepository.Setup(repository => repository.GetByIdAsync(conversationId))
            .ReturnsAsync(conversation);
        _userRepository.Setup(userRepo=>userRepo.HasClaimAsync(_userId, Claims.CONVERSATIONS_RESPOND, Claims.CONVERSATIONS_RESPOND))
            .ReturnsAsync(true);
        
        // Act
        var result = await _messageService.SendMessage(conversationId, "Message");
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _conversationRepository.Verify(conversationRepository=> conversationRepository.AddMessageAsync(It.Is<Message>(msg=>
            msg.ConversationId == conversationId &&
            msg.Content == "Message" &&
            msg.UserId == _userId
        )), Times.Once());
    }
}