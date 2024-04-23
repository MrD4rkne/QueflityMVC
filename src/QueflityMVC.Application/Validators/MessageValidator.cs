using FluentValidation;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Validators;

public class MessageValidator : AbstractValidator<MessageVm>
{
    private const int MESSAGE_MIN_LENGTH = 40;

    public MessageValidator()
    {
        RuleFor(x => x.Purchasable)
            .NotNull();
        RuleFor(x => x.Message)
            .MinimumLength(MESSAGE_MIN_LENGTH);
    }
}