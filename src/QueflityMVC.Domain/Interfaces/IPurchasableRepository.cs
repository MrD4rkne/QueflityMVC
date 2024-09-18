using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IProductRepository
{
    Task<bool> AreTheseAllVisibleProductsAsync(List<Product> productModels);

    Task<uint> GetNextOrderNumberAsync();

    IQueryable<Product> GetVisibleEntities();

    Task UpdateOrderNoAsync(Product product);

    Task UpdateProductsOrderAsync(List<Product> productModels);

    IQueryable<Product> GetVisibleProductsForDashboard();

    Task<Product?> GetByIdAsync(int id);
    
    Task BulkUpdateOrderAsync(uint orderNo);
}