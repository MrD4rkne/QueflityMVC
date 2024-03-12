using FluentValidation;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Validators;

public class ItemValidator : AbstractValidator<ItemVm>
{
    public ItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30)
            .Matches("[A-Za-z]*")
            .WithMessage("Name can only contain letters");
        RuleFor(x => x.Price)
            .GreaterThanOrEqualTo(0);
        RuleFor(x => x.Image)
            .NotNull()
            !.SetValidator(new ImageValidator());
    }
}