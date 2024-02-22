using FluentAssertions;
using FluentValidation;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Category;
using Xunit;

namespace QueflityMVC.Test.Web.Validators;

public class CategoryValidatorTest
{
    private static CategoryVM GetPerfectCategoryVM()
    {
        CategoryVM categoryVM = new() { Id = 0, Name = "123456" };
        return categoryVM;
    }

    [Fact]
    public void ValidationTest()
    {
        CategoryVM nullName = GetPerfectCategoryVM();
        nullName.Name = null;
        CategoryVM emptyName = GetPerfectCategoryVM();
        emptyName.Name = string.Empty;
        CategoryVM tooShort = GetPerfectCategoryVM();
        tooShort.Name = "1";
        CategoryVM minimumLength = GetPerfectCategoryVM();
        minimumLength.Name = "12";
        CategoryVM validLength = GetPerfectCategoryVM();
        validLength.Name = "123456";
        CategoryVM maximumLength = GetPerfectCategoryVM();
        maximumLength.Name = "12345678912345678912";
        CategoryVM tooLong = GetPerfectCategoryVM();
        tooLong.Name = "123456789123456789123456789";

        IValidator<CategoryVM> validator = new CategoryValidator();

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