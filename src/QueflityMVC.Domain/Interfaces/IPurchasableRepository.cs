using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;

public interface IProductRepository
{
    Task<bool> AreTheseAllVisibleProductsAsync(List<Product> purchasableModels);

    Task<uint> GetNextOrderNumberAsync();

    IQueryable<Product> GetVisibleEntities();

    Task UpdateOrderNoAsync(Product purchasable);

    Task UpdateProductsOrderAsync(List<Product> purchasableModels);

    IQueryable<Product> GetVisibleProductsForDashboard();

    Task<Product?> GetByIdAsync(int id);
}