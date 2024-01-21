using FluentValidation;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Validators
{
    public class ItemValidator : AbstractValidator<ItemDTO>
    {
        public ItemValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
            RuleFor(x => x.Image)
                .NotNull()
                ?.SetValidator(new ImageValidator());
        }
    }
}