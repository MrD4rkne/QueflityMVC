using QueflityMVC.Application.ViewModels.Product;

namespace QueflityMVC.Application.ViewModels.Other;

public record MessageVm
{
    public required ProductForDashboardVm Product { get; init; }

    public string? Message { get; init; }

    public string? Email { get; init; }
}