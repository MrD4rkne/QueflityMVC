using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Options;

namespace QueflityMVC.Web.Setup.Mails;

public record SmtpOptions
{
    public const string SECTION_NAME = "Smtp";

    [Required] public required string Host { get; init; }

    [Required] [Range(1, 65535)] public required int Port { get; init; }

    [Required] public required string Username { get; init; }

    [Required] public required string Password { get; init; }

    [Required] public required string Email { get; init; }
}

[OptionsValidator]
public partial class SmtpOptionsValidator : IValidateOptions<SmtpOptions>
{
}