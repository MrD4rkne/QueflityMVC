#region

using QueflityMVC.Application.ViewModels.Product;

#endregion

namespace QueflityMVC.Application.ViewModels.Other;

public record FirstMessageInConversationVm
{
    public required ProductForCardVm Product { get; init; }

    public string? Message { get; init; }

    public string? Email { get; init; }

    public string Title { get; set; }
}