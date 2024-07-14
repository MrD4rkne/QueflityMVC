using FluentValidation;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Validators;

public class KitValidator : AbstractValidator<KitVm>
{
    private const string REGEX_ONLY_LETTERS = "[A-Za-z- ]*";
    private const string REGEX_LETTERS_AND_BASIC_SYNTAX = "^[a-zA-Z0-9,.\\s]+$";

    public KitValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30)
            .Matches(REGEX_ONLY_LETTERS).WithMessage("Name can only contain letters");
        RuleFor(x => x.Image)
            .NotNull()
            !.SetValidator(new ImageValidator());
        RuleFor(x => x.Description)
            .Matches(REGEX_LETTERS_AND_BASIC_SYNTAX).WithMessage("Description can only contain letters");
        RuleFor(x => x.ShouldBeShown)
            .Must(x=>!x).When(kit=>kit.ElementCount==0).WithMessage("Kit should be hidden if empty");
    }
}