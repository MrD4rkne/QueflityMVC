using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup.Other;

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

[OptionsValidator]
internal partial class BrandOptionsValidator : IValidateOptions<BrandOptions>;