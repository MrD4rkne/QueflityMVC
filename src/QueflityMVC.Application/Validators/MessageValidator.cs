using FluentValidation;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Validators;

public class MessageValidator : AbstractValidator<MessageVm>
{
    private const int MESSAGE_MIN_LENGTH = 40;
    private const int MESSAGE_MAX_LENGTH = 400;

    public MessageValidator()
    {
        RuleFor(x => x.Product)
            .NotNull();
        RuleFor(x => x.Message)
            .MinimumLength(MESSAGE_MIN_LENGTH)
            .MaximumLength(MESSAGE_MAX_LENGTH);
    }
}