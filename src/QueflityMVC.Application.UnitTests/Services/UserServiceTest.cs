using AutoMapper;
using Moq;
using QueflityMVC.Application.Constants;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.UnitTests.Common;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class UserServiceTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<IMapper> _mapper;
    private readonly Mock<IUserContext> _userContext;

    private readonly IUserService _userService;

    private readonly Guid _userId;

    public UserServiceTest()
    {
        _userId = Guid.NewGuid();

        _userContext = new Mock<IUserContext>();
        _userContext.Setup(ctx => ctx.UserId)
            .Returns(_userId);
        _userContext.Setup(ctx => ctx.IsAuthenticated)
            .Returns(true);

        _userRepository = new Mock<IUserRepository>();
        _mapper = new Mock<IMapper>();
        
        _userService = new UserService(_userRepository.Object, _mapper.Object, _userContext.Object);
    }
    
    [Fact]
    public async Task DisableUserAsync_WhenDisablingSelf_ReturnsFailure()
    {
        // Arrange
        var user = new ApplicationUser
        {
            Id = _userId,
            IsEnabled = true
        };
        _userRepository.Setup(repo => repo.GetUserByIdAsync(_userId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.DisableUserAsync(_userId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.User.CannotManageThemselves);
    }
    
    [Fact]
    public async Task DisableUserAsync_OnUserDoesNotExist_ReturnsFailure()
    {
        // Arrange
        Guid userToDisableId = _userId.GetDifferentGuid();

        _userRepository.Setup(repo => repo.GetUserByIdAsync(userToDisableId))
            .ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await _userService.DisableUserAsync(userToDisableId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.User.DoesNotExist);
    }
    
    [Fact]
    public async Task DisableUserAsync_ReturnsSuccess()
    {
        // Arrange
        Guid userToDisableId = _userId.GetDifferentGuid();
        var user = new ApplicationUser
        {
            Id = userToDisableId,
            IsEnabled = true
        };

        _userRepository.Setup(repo => repo.GetUserByIdAsync(userToDisableId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.DisableUserAsync(userToDisableId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _userRepository.Verify(repo => repo.UpdateAsync(It.Is<ApplicationUser>(user=>
            user.Id == userToDisableId && user.IsEnabled == false
            )), Times.Once);
    }
    
    [Fact]
    public async Task EnableUserAsync_WhenEnablingSelf_ReturnsFailure()
    {
        // Arrange
        var user = new ApplicationUser
        {
            Id = _userId,
            IsEnabled = true
        };
        _userRepository.Setup(repo => repo.GetUserByIdAsync(_userId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.EnableUserAsync(_userId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.User.CannotManageThemselves);
    }
    
    [Fact]
    public async Task EnableUserAsync_OnUserDoesNotExist_ReturnsFailure()
    {
        // Arrange
        Guid userToDisableId = _userId.GetDifferentGuid();

        _userRepository.Setup(repo => repo.GetUserByIdAsync(userToDisableId))
            .ReturnsAsync((ApplicationUser)null);

        // Act
        var result = await _userService.EnableUserAsync(userToDisableId);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.User.DoesNotExist);
    }
    
    [Fact]
    public async Task EnableUserAsync_ReturnsSuccess()
    {
        // Arrange
        Guid userToDisableId = _userId.GetDifferentGuid();
        var user = new ApplicationUser
        {
            Id = userToDisableId,
            IsEnabled = false
        };

        _userRepository.Setup(repo => repo.GetUserByIdAsync(userToDisableId))
            .ReturnsAsync(user);

        // Act
        var result = await _userService.EnableUserAsync(userToDisableId);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _userRepository.Verify(repo => repo.UpdateAsync(It.Is<ApplicationUser>(user=>
            user.Id == userToDisableId && user.IsEnabled == true
            )), Times.Once);
    }

    [Fact]
    public async Task Update_UpdateUserClaimsAsync_OnInvalidClaim_ReturnsError()
    {
        // Arrange
        Guid userToManageId = _userId.GetDifferentGuid();
        var user = new ApplicationUser
        {
            Id = userToManageId,
            UserName = "TestUser",
            IsEnabled = true
        };
        _userRepository.Setup(repo => repo.GetUserByIdAsync(_userId))
            .ReturnsAsync(user);
        
        var allClaimsWithAdditional = Claims.GetAll()
            .Select(str => new ClaimForSelectionVm(str))
            .ToList();
        
        ClaimForSelectionVm invalidClaim = new("InvalidClaim")
        {
            IsSelected = true
        };
        allClaimsWithAdditional.Add(invalidClaim);

        var userClaimsVm = new UserClaimsVm
        {
            UserId = userToManageId,
            IsEnabled = true,
            Username = user.UserName,
            AllClaims = allClaimsWithAdditional,
            AssignedClaimsIds=[]
        };

        // Act
        var result = await _userService.UpdateUserClaimsAsync(userClaimsVm);

        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.ShouldBe(Errors.Claims.DoesNotExist);
        
        _userRepository.Verify(repo => repo.UpdateClaimsAsync(_userId, It.IsAny<string[]>()), Times.Never);
    }
    
    [Fact]
    public async Task Update_UpdateUserClaimsAsync_ReturnsSuccess()
    {
        // Arrange
        Guid userToManageId = _userId.GetDifferentGuid();
        var user = new ApplicationUser
        {
            Id = userToManageId,
            UserName = "TestUser",
            IsEnabled = true
        };
        _userRepository.Setup(repo => repo.GetUserByIdAsync(_userId))
            .ReturnsAsync(user);
        
        var allClaimsWithAdditional = Claims.GetAll()
            .Select(str => new ClaimForSelectionVm(str)
            {
                IsSelected = Random.Shared.Next(0, 2) == 1
            })
            .ToList();
        
        var claimsToGive = allClaimsWithAdditional
            .Where(x => x.IsSelected)
            .Select(x => x.Id)
            .Distinct()
            .ToArray();

        var userClaimsVm = new UserClaimsVm
        {
            UserId = userToManageId,
            IsEnabled = true,
            Username = user.UserName,
            AllClaims = allClaimsWithAdditional,
            AssignedClaimsIds=[]
        };

        // Act
        var result = await _userService.UpdateUserClaimsAsync(userClaimsVm);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        
        _userRepository.Verify(repo => repo.UpdateClaimsAsync(userToManageId, It.Is<string[]>(
            array=> array.SequenceEqual(claimsToGive))), Times.Once);
    }
}