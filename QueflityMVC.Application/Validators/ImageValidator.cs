using FluentValidation;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Validators
{
    public class ImageValidator : AbstractValidator<ImageDTO>
    {
        public ImageValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("'Title' może zawierać tylko litery");
            RuleFor(x => x.AltDescription)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("'AltDescription' może zawierać tylko litery");
            RuleFor(x => x.FormFile)
                .NotNull().WithMessage("Musisz wysłać zdjęcie").When(x=>string.IsNullOrEmpty(x.FileUrl));

        }
    }
}
