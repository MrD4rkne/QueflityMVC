using FluentValidation;
using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Application.Validators
{
    public class ItemCategoryValidator : AbstractValidator<ItemCategoryDTO>
    {
        public ItemCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
        }
    }
}
