using FluentValidation;
using FluentValidation.Results;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Validators;

public class MessageValidator : AbstractValidator<ContactVm>
{
    private const int MESSAGE_MIN_LENGTH = 40;
    public MessageValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress();
        RuleFor(x => x.Purchasable)
            .NotNull();
        RuleFor(x => x.Message)
            .MinimumLength(MESSAGE_MIN_LENGTH);
        // TODO: add must login for sending message
    }
}