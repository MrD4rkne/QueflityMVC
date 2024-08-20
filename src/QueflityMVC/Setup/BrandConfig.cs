using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup.Other;

public class BrandOptions
{
    public const string SECTION_NAME = "Brand";
    
    [Required]
    public string Name { get; set; }
}

[OptionsValidator]
internal partial class BrandOptionsValidator : IValidateOptions<BrandOptions>;