using FluentAssertions;
using FluentValidation;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Element;
using Xunit;

namespace QueflityMVC.Test.Web.Validators;

public class ElementValidatorTest
{
    public ElementVM GetPerfectVM()
    {
        ElementVM elementVM = new()
        {
            Id = 0,
            ItemsAmmount = 2,
            PricePerItem = 3,
            Item = null,
            KitDetailsVM = null
        };
        return elementVM;
    }

    [Fact]
    public void PriceValidation()
    {
        ElementVM tooLow = GetPerfectVM();
        tooLow.PricePerItem = (decimal)(0 - 0.01);
        ElementVM minimum = GetPerfectVM();
        minimum.PricePerItem = (decimal)(0);
        ElementVM valid = GetPerfectVM();
        valid.PricePerItem = (decimal)(5);
        IValidator<ElementVM> validator = new ElementValidator();

        var lowResults = validator.Validate(tooLow);
        var minResults = validator.Validate(minimum);
        var validResults = validator.Validate(valid);

        lowResults.Should().NotBeNull();
        lowResults.IsValid.Should().BeFalse();
        minResults.Should().NotBeNull();
        minResults.IsValid.Should().BeTrue();
        validResults.Should().NotBeNull();
        validResults.IsValid.Should().BeTrue();
    }
}