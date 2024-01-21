using FluentValidation;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.Validators
{
    public class ImageValidator : AbstractValidator<ImageDTO?>
    {
        public ImageValidator()
        {
            RuleFor(x => x).NotNull();
            RuleFor(x => x!.AltDescription)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("Alternative description can only contain letters");
            RuleFor(x => x!.FormFile)
                .NotNull().WithMessage("Image must be attached").When(x => string.IsNullOrEmpty(x!.FileUrl));
        }
    }
}