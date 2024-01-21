using FluentValidation;
using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.Validators
{
    public class IngredientValidator : AbstractValidator<IngredientDTO>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
        }
    }
}