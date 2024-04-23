using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.ViewModels.Other;

public record MessageVm
{
    public required PurchasableForDashboardVm Purchasable { get; init; }

    public string? Message { get; init; }
}