using AutoMapper;
using FluentAssertions;
using Moq;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Domain.Interfaces;
using Xunit;

namespace QueflityMVC.Test.Web.Services;

public class UserServiceTest
{
    [Fact]
    public async void DisableUserTest()
    {
        var existingId = "1";
        var nonExistingId = "D";
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.DoesUserExistAsync(existingId)).ReturnsAsync(true);
        userRepository.Setup(x => x.DoesUserExistAsync(nonExistingId)).ReturnsAsync(false);

        var mapper = new Mock<IMapper>();
        //userRepository.Setup(x => x.DisableUser(existingId)).Returns(Task.CompletedTask);
        IUserService userService = new UserService(userRepository.Object, mapper.Object);

        var existingAct = async () => { await userService.DisableUserAsync(existingId); };
        var nonExistingAct = async () => { await userService.DisableUserAsync(nonExistingId); };

        await nonExistingAct.Should().ThrowAsync<EntityNotFoundException>();
        await existingAct.Should().NotThrowAsync<EntityNotFoundException>();
    }

    [Fact]
    public async void EnableUserTest()
    {
        var existingId = "1";
        var nonExistingId = "D";
        var userRepository = new Mock<IUserRepository>();
        userRepository.Setup(x => x.DoesUserExistAsync(existingId)).ReturnsAsync(true);
        userRepository.Setup(x => x.DoesUserExistAsync(nonExistingId)).ReturnsAsync(false);
        IUserService userService = new UserService(userRepository.Object, null);

        var existingAct = async () => { await userService.EnableUserAsync(existingId); };
        var nonExistingAct = async () => { await userService.EnableUserAsync(nonExistingId); };

        await nonExistingAct.Should().ThrowAsync<EntityNotFoundException>();
        await existingAct.Should().NotThrowAsync<EntityNotFoundException>();
    }
}