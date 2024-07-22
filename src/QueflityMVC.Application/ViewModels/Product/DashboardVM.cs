namespace QueflityMVC.Application.ViewModels.Product;

public record DashboardVm
{
    public required List<ProductForDashboardVm> Products { get; set; }
}