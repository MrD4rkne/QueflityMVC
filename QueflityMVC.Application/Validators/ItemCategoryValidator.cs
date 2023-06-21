using FluentValidation;
using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                .Matches("[A-Za-z]*").WithMessage("'Name' może zawierać tylko litery");
        }
    }
}
