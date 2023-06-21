using FluentValidation;
using QueflityMVC.Application.ViewModels.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Matches("[A-Za-z]*").WithMessage("'Name' może zawierać tylko litery");
            RuleFor(x => x.Image)
                .NotNull()
                ?.SetValidator(new ImageValidator());
        }
    }
}
