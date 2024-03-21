namespace QueflityMVC.Application.ViewModels.Purchasable;

public record ContactVm
{
    public required PurchasableForDashboardVm Purchasable { get; init; }
    
    public string? Email { get; init; }
    
    public string? Message { get; init; }
}