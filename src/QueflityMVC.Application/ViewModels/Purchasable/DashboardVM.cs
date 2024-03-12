namespace QueflityMVC.Application.ViewModels.Purchasable;

public record DashboardVm
{
    public required List<PurchasableForDashboardVm> Purchasables { get; init; }
}