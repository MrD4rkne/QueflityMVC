namespace QueflityMVC.Application.ViewModels.Product;

public record EditOrderVm
{
    public required List<ProductVm> ProductsVMs { get; set; }
}