namespace QueflityMVC.Application.ViewModels.Purchasable;

public record EditOrderVm
{
    public required List<PurchasableVm> PurchasablesVMs { get; set; }
}