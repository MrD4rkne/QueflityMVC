using System.ComponentModel.DataAnnotations;

namespace QueflityMVC.Web.Setup;

internal class GoogleOAuthOptions
{
    public const string SECTION_NAME = "GoogleOAuth";

    [Required] public string ClientId { get; set; }

    [Required] public string ClientSecret { get; set; }
}