using FluentAssertions;
using FluentValidation;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Element;
using Xunit;

namespace QueflityMVC.Test.Web.Validators;

public class ElementValidatorTest
{
    public ElementVm GetPerfectVm()
    {
        ElementVm elementVm = new()
        {
            Id = 0,
            ItemsAmmount = 2,
            PricePerItem = 3,
            Item = null,
            KitDetailsVm = null
        };
        return elementVm;
    }

    [Fact]
    public void PriceValidation()
    {
        var tooLow = GetPerfectVm();
        tooLow.PricePerItem = (decimal)(0 - 0.01);
        var minimum = GetPerfectVm();
        minimum.PricePerItem = 0;
        var valid = GetPerfectVm();
        valid.PricePerItem = 5;
        IValidator<ElementVm> validator = new ElementValidator();

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