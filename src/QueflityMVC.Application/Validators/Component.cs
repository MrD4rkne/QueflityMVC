using FluentValidation;
using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.Validators;

public class ComponentValidator : AbstractValidator<ComponentVM>
{
    public ComponentValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20)
            .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
    }
}