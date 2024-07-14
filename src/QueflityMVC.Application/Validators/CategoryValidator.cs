using FluentValidation;
using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Validators;

public class CategoryValidator : AbstractValidator<CategoryVm>
{
    private const string REGEX_ONLY_LETTERS = "[A-Za-z- ]*";

    public CategoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20)
            .Matches(REGEX_ONLY_LETTERS).WithMessage("Name can only contain letters");
    }
}