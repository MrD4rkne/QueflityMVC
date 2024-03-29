﻿using FluentValidation;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Validators;

public class KitValidator : AbstractValidator<KitVM>
{
    public KitValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(30)
            .Matches("[A-Za-z]*").WithMessage("Name can only contain letters");
        RuleFor(x => x.Image)
            .NotNull()
            !.SetValidator(new ImageValidator());
        RuleFor(x => x.Description)
            .Matches("[A-Za-z]*").WithMessage("Description can only contain letters");
    }
}