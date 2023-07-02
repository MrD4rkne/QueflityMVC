using FluentValidation;
using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Application.Validators
{
    public class ItemSetValidator : AbstractValidator<ItemSetDTO>
    {
        public ItemSetValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("'Name' może zawierać tylko litery");
            RuleFor(x => x.Image)
                .NotNull()
                ?.SetValidator(new ImageValidator());
        }
    }
}
