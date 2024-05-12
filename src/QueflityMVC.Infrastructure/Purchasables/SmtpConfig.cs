namespace QueflityMVC.Infrastructure.Purchasables;

public record SmtpConfig
{
    public required string Host { get; init; }

    public required int Port { get; init; }

    public required string Username { get; init; }

    public required string Password { get; init; }
}