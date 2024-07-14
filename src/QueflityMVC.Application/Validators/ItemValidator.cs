using FluentValidation;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Validators;

public class ItemValidator : AbstractValidator<ItemVm>
{
    private const string REGEX_ONLY_LETTERS = "[A-Za-z- ]*";

    public ItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30)
            .Matches(REGEX_ONLY_LETTERS)
            .WithMessage("Name can only contain letters");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Image)
            .NotNull()
            !.SetValidator(new ImageValidator());
    }
}