namespace QueflityMVC.Web.Areas.Identity.Pages.Account;

public record PopUpViewModel
{
    public string? Title { get; init; }

    public required string Message { get; init; }

    public PopUpType Type { get; init; }
}

public enum PopUpType
{
    Success,
    Error,
    Warning,
    Info
}