using FluentValidation;
using QueflityMVC.Application.ViewModels.Element;

namespace QueflityMVC.Application.Validators;

public class ElementValidator : AbstractValidator<ElementVM>
{
    public ElementValidator()
    {
        RuleFor(elem => elem.PricePerItem)
            .GreaterThanOrEqualTo(0);
        RuleFor(elem => elem.ItemsAmmount)
            .Must(quantity => quantity > 0);
    }
}