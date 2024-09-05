using System.Net.Sockets;
using Microsoft.Extensions.Logging;
using Moq;
using QueflityMVC.Application.Emails;
using QueflityMVC.Application.Services;
using QueflityMVC.Domain.Models;
using Shouldly;

namespace QueflityMVC.Application.UnitTests.Services;

public class EmailServiceTests
{
    [Fact]
    public async Task SendEmailConfirmationAsync_WhenEmailSenderSucceeds_ReturnsSuccess()
    {
        // Arrange
        var email = "dummy@email.com";

        var emailSender = new Mock<IEmailSender>();
        emailSender.Setup(x => x.SendEmailConfirmationAsync(It.IsAny<EmailConfirmation>()))
            .Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<EmailService>>();

        var emailService = new EmailService(emailSender.Object, logger.Object);

        var emailConfirmation = new EmailConfirmation
        {
            Email = email,
            Url = "https://dummy.com/confirm",
            User = new ApplicationUser
            {
                UserName = "dummy",
                Email = email
            }
        };

        // Act
        var result = await emailService.SendEmailConfirmationAsync(emailConfirmation);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        emailSender.Verify(x => x.SendEmailConfirmationAsync(emailConfirmation), Times.Once);
    }

    [Fact]
    public async Task SendEmailConfirmationAsync_WhenEmailSenderFails_ReturnsFailure()
    {
        // Arrange
        var email = "dummy@email.com";

        var emailSender = new Mock<IEmailSender>();
        emailSender.Setup(x => x.SendEmailConfirmationAsync(It.IsAny<EmailConfirmation>()))
            .ThrowsAsync(new SocketException());

        var logger = new Mock<ILogger<EmailService>>();

        var emailService = new EmailService(emailSender.Object, logger.Object);

        var emailConfirmation = new EmailConfirmation
        {
            Email = email,
            Url = "https://dummy.com/confirm",
            User = new ApplicationUser
            {
                UserName = "dummy",
                Email = email
            }
        };

        // Act
        var result = await emailService.SendEmailConfirmationAsync(emailConfirmation);

        // Assert
        result.IsFailure.ShouldBeTrue();
        emailSender.Verify(x => x.SendEmailConfirmationAsync(emailConfirmation), Times.Once);
    }

    [Fact]
    public async Task SendResetPasswordEmail_WhenEmailSenderSucceeds_ReturnsSuccess()
    {
        // Arrange
        var email = "dummy@email.com";

        var emailSender = new Mock<IEmailSender>();
        emailSender.Setup(x => x.SendResetPasswordEmail(It.IsAny<ResetPasswordEmail>()))
            .Returns(Task.CompletedTask);

        var logger = new Mock<ILogger<EmailService>>();

        var emailService = new EmailService(emailSender.Object, logger.Object);

        var passwordResetEmail = new ResetPasswordEmail
        {
            Email = email,
            Url = "https://dummy.com/confirm",
            User = new ApplicationUser
            {
                UserName = "dummy",
                Email = email
            }
        };

        // Act
        var result = await emailService.SendResetPasswordEmail(passwordResetEmail);

        // Assert
        result.IsSuccess.ShouldBeTrue();
        emailSender.Verify(x => x.SendResetPasswordEmail(passwordResetEmail), Times.Once);
    }

    [Fact]
    public async Task SendResetPasswordEmail_WhenEmailSenderFails_ReturnsFailure()
    {
        // Arrange
        var email = "dummy@email.com";

        var emailSender = new Mock<IEmailSender>();
        emailSender.Setup(x => x.SendResetPasswordEmail(It.IsAny<ResetPasswordEmail>()))
            .ThrowsAsync(new SocketException());

        var logger = new Mock<ILogger<EmailService>>();

        var emailService = new EmailService(emailSender.Object, logger.Object);

        var passwordResetEmail = new ResetPasswordEmail
        {
            Email = email,
            Url = "https://dummy.com/confirm",
            User = new ApplicationUser
            {
                UserName = "dummy",
                Email = email
            }
        };

        // Act
        var result = await emailService.SendResetPasswordEmail(passwordResetEmail);

        // Assert
        result.IsFailure.ShouldBeTrue();
        emailSender.Verify(x => x.SendResetPasswordEmail(passwordResetEmail), Times.Once);
    }
}