using FluentValidation;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.Validators;

public class ImageValidator : AbstractValidator<ImageVm?>
{
    private const string REGEX_ONLY_LETTERS = "[A-Za-z- ]*";

    public ImageValidator()
    {
        RuleFor(x => x).NotNull();
        RuleFor(x => x!.AltDescription)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20)
            .WithName("Alternative description")
            .Matches(REGEX_ONLY_LETTERS).WithMessage("Alternative description can only contain letters");
        RuleFor(x => x!.FormFile)
            .NotNull().WithMessage("Image must be attached").When(x => string.IsNullOrEmpty(x!.FileUrl));
    }
}