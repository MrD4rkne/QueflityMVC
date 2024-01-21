using FluentValidation;
using QueflityMVC.Application.ViewModels.Element;

namespace QueflityMVC.Application.Validators
{
    public class ElementValidator : AbstractValidator<ElementDTO>
    {
        public ElementValidator()
        {
            RuleFor(elem => elem.PricePerItem)
                .Must(price => price >= 0);
            RuleFor(elem => elem.ItemsAmmount)
                .Must(quantity => quantity > 0);
        }
    }
}