using FluentAssertions;
using FluentValidation;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Category;
using Xunit;

namespace QueflityMVC.Test.Web.Validators;

public class CategoryValidatorTest
{
    private static CategoryVm GetPerfectCategoryVm()
    {
        CategoryVm categoryVm = new() { Id = 0, Name = "123456" };
        return categoryVm;
    }

    [Fact]
    public void ValidationTest()
    {
        var nullName = GetPerfectCategoryVm();
        nullName.Name = null;
        var emptyName = GetPerfectCategoryVm();
        emptyName.Name = string.Empty;
        var tooShort = GetPerfectCategoryVm();
        tooShort.Name = "1";
        var minimumLength = GetPerfectCategoryVm();
        minimumLength.Name = "12";
        var validLength = GetPerfectCategoryVm();
        validLength.Name = "123456";
        var maximumLength = GetPerfectCategoryVm();
        maximumLength.Name = "12345678912345678912";
        var tooLong = GetPerfectCategoryVm();
        tooLong.Name = "123456789123456789123456789";

        IValidator<CategoryVm> validator = new CategoryValidator();

        var nullResults = validator.Validate(nullName);
        var emptyResults = validator.Validate(emptyName);
        var tooShortResults = validator.Validate(tooShort);
        var minimumResults = validator.Validate(minimumLength);
        var validResults = validator.Validate(validLength);
        var maximumResults = validator.Validate(maximumLength);
        var tooLongResults = validator.Validate(tooLong);

        nullResults.Should().NotBeNull();
        nullResults.IsValid.Should().BeFalse();
        emptyResults.Should().NotBeNull();
        emptyResults.IsValid.Should().BeFalse();
        tooShortResults.Should().NotBeNull();
        tooShortResults.IsValid.Should().BeFalse();
        minimumResults.Should().NotBeNull();
        minimumResults.IsValid.Should().BeTrue();
        validResults.Should().NotBeNull();
        validResults.IsValid.Should().BeTrue();
        maximumResults.Should().NotBeNull();
        maximumResults.IsValid.Should().BeTrue();
        tooLongResults.Should().NotBeNull();
        tooLongResults.IsValid.Should().BeFalse();
    }
}