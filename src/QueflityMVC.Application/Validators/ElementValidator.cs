using FluentValidation;
using QueflityMVC.Application.ViewModels.Element;

namespace QueflityMVC.Application.Validators;

public class ElementValidator : AbstractValidator<ElementVm>
{
    public ElementValidator()
    {
        RuleFor(elem => elem.PricePerItem)
            .GreaterThanOrEqualTo(0);
        RuleFor(elem => elem.ItemsAmount)
            .Must(quantity => quantity > 0);
    }
}