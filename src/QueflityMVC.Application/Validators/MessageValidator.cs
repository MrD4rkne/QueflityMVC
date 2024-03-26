using FluentValidation;
using FluentValidation.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Purchasable;

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