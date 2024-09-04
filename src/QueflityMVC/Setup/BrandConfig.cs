using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup;

public class BrandOptions
{
    public const string SECTION_NAME = "Brand";

    [Required] public string Name { get; set; }

    [Required] public LoginPageOptions IdentityPage { get; set; }

    public class LoginPageOptions
    {
        [Required] public string Header { get; set; }

        [Required] public string Subheader { get; set; }

        [Required] public string BackgroundImageUrl { get; set; }
    }
}

internal partial class BrandOptionsValidator : IValidateOptions<BrandOptions>
{
    public ValidateOptionsResult Validate(string? name, BrandOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.Name))
        {
            return ValidateOptionsResult.Fail("Name must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.IdentityPage.Header))
        {
            return ValidateOptionsResult.Fail("Header must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.IdentityPage.Subheader))
        {
            return ValidateOptionsResult.Fail("Subheader must be provided.");
        }

        if (string.IsNullOrWhiteSpace(options.IdentityPage.BackgroundImageUrl))
        {
            return ValidateOptionsResult.Fail("Background image URL must be provided.");
        }

        return ValidateOptionsResult.Success;
    }
}