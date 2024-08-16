namespace QueflityMVC.Application.ViewModels.Product;

public record DashboardVm
{
    public required List<ProductForCardVm> Products { get; set; }
}