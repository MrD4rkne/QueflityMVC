using FluentValidation;
using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.Validators;

public class ComponentValidator : AbstractValidator<ComponentVm>
{
    private const string REGEX_ONLY_LETTERS = "[A-Za-z- ]*";

    public ComponentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20)
            .Matches(REGEX_ONLY_LETTERS).WithMessage("Name can only contain letters");
    }
}