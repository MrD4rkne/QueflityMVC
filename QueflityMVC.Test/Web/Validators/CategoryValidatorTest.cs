using FluentAssertions;
using FluentValidation;
using QueflityMVC.Application.Validators;
using QueflityMVC.Application.ViewModels.Category;
using Xunit;

namespace QueflityMVC.Test.Web.Validators;

public class CategoryValidatorTest
{
    private static CategoryDTO GetPerfectCategoryDTO()
    {
        CategoryDTO categoryDTO = new() { Id = 0, Name = "123456" };
        return categoryDTO;
    }

    [Fact]
    public void ValidationTest()
    {
        CategoryDTO nullName = GetPerfectCategoryDTO();
        nullName.Name = null;
        CategoryDTO emptyName = GetPerfectCategoryDTO();
        emptyName.Name = string.Empty;
        CategoryDTO tooShort = GetPerfectCategoryDTO();
        tooShort.Name = "1";
        CategoryDTO minimumLength = GetPerfectCategoryDTO();
        minimumLength.Name = "12";
        CategoryDTO validLength = GetPerfectCategoryDTO();
        validLength.Name = "123456";
        CategoryDTO maximumLength = GetPerfectCategoryDTO();
        maximumLength.Name = "12345678912345678912";
        CategoryDTO tooLong = GetPerfectCategoryDTO();
        tooLong.Name = "123456789123456789123456789";

        IValidator<CategoryDTO> validator = new CategoryValidator();

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