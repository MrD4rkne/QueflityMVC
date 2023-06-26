using FluentValidation;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Application.ViewModels.ItemCategory;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Validators
{
    public class IngredientValidator : AbstractValidator<IngredientDTO>
    {
        public IngredientValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(20)
                .Matches("[A-Za-z]*").WithMessage("'Name' can only contain letters");
        }
    }
}
