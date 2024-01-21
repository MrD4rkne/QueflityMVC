using FluentValidation;
using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Validators
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
        }
    }
}