using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;

public interface IPurchasableRepository
{
    Task<bool> AreTheseAllVisiblePurchasablesAsync(List<Product> purchasableModels);

    Task<uint> GetNextOrderNumberAsync();

    IQueryable<Product> GetVisibleEntities();

    Task UpdateOrderNoAsync(Product purchasable);

    Task UpdatePurchasablesOrderAsync(List<Product> purchasableModels);

    IQueryable<Product> GetVisiblePurchasablesForDashboard();

    Task<Product?> GetByIdAsync(int id);
}