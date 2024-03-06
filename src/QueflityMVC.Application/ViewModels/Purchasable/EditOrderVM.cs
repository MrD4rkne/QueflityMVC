namespace QueflityMVC.Application.ViewModels.Purchasable;
public record EditOrderVM
{
    public required List<PurchasableVM> PurchasablesVMs { get; set; }
}