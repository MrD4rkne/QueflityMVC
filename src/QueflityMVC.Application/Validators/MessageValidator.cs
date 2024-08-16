using FluentValidation;
using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.Validators;

public class MessageValidator : AbstractValidator<FirstMessageInConversationVm>
{
    private const int MESSAGE_MIN_LENGTH = 10;
    private const int MESSAGE_MAX_LENGTH = 400;

    public MessageValidator()
    {
        RuleFor(x => x.Product)
            .NotNull();
        RuleFor(x => x.Message)
            .MinimumLength(MESSAGE_MIN_LENGTH)
            .MaximumLength(MESSAGE_MAX_LENGTH);
        RuleFor(x => x.Title)
            .NotEmpty();
    }
}