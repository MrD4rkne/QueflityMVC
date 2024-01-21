using AutoMapper;
using FluentAssertions;
using Moq;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Services;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using Xunit;

namespace QueflityMVC.Test.Web.Services
{
    public class UserServiceTest
    {
        [Fact]
        public async void DisableUserTest()
        {
            string emptyId = string.Empty;
            string nullId = null;
            string existingId = "1";
            string nonExistingId = "D";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.DoesUserExist(existingId)).ReturnsAsync(true);
            userRepository.Setup(x => x.DoesUserExist(nonExistingId)).ReturnsAsync(false);
            //userRepository.Setup(x => x.DisableUser(existingId)).Returns(Task.CompletedTask);
            IUserService userService = new UserService(userRepository.Object, null);

            Func<Task> emptyAct = async () => { await userService.DisableUser(emptyId); };
            Func<Task> nullAct = async () => { await userService.DisableUser(nullId); };
            Func<Task> existingAct = async () => { await userService.DisableUser(existingId); };
            Func<Task> nonExistingAct = async () => { await userService.DisableUser(nonExistingId); };

            await emptyAct.Should().ThrowAsync<ArgumentException>();
            await nullAct.Should().ThrowAsync<ArgumentNullException>();
            await nonExistingAct.Should().ThrowAsync<EntityNotFoundException>();
            await existingAct.Should().NotThrowAsync<ArgumentNullException>();
            await existingAct.Should().NotThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void EnableUserTest()
        {
            string emptyId = string.Empty;
            string nullId = null;
            string existingId = "1";
            string nonExistingId = "D";
            var userRepository = new Mock<IUserRepository>();
            userRepository.Setup(x => x.DoesUserExist(existingId)).ReturnsAsync(true);
            userRepository.Setup(x => x.DoesUserExist(nonExistingId)).ReturnsAsync(false);
            //userRepository.Setup(x => x.DisableUser(existingId)).Returns(Task.CompletedTask);
            IUserService userService = new UserService(userRepository.Object, null);

            Func<Task> emptyAct = async () => { await userService.EnableUser(emptyId); };
            Func<Task> nullAct = async () => { await userService.EnableUser(nullId); };
            Func<Task> existingAct = async () => { await userService.EnableUser(existingId); };
            Func<Task> nonExistingAct = async () => { await userService.EnableUser(nonExistingId); };

            await emptyAct.Should().ThrowAsync<ArgumentException>();
            await nullAct.Should().ThrowAsync<ArgumentNullException>();
            await nonExistingAct.Should().ThrowAsync<EntityNotFoundException>();
            await existingAct.Should().NotThrowAsync<ArgumentNullException>();
            await existingAct.Should().NotThrowAsync<EntityNotFoundException>();
        }

        [Fact]
        public async void GetFilteredListTest()
        {
            ListUsersVM nullVM = null;
            ListUsersVM nullPagination = new ListUsersVM() { Pagination = null };
            var userRepository = new Mock<IUserRepository>();
            var mapper = new Mock<IMapper>();
            IUserService userService = new UserService(userRepository.Object, mapper.Object);

            Func<Task<ListUsersVM>> nullAct = async () => { return await userService.GetFilteredList(nullVM); };
            Func<Task<ListUsersVM>> nullPaginationAct = async () => { return await userService.GetFilteredList(nullPagination); };

            await nullAct.Should().ThrowAsync<ArgumentNullException>();
            await nullPaginationAct.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}