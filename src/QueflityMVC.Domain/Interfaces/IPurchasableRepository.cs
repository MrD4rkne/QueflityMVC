using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;

public interface IPurchasableRepository
{
    Task<bool> AreTheseAllVisiblePurchasablesAsync(List<BasePurchasableEntity> purchasableModels);

    Task BulkUpdateOrderAsync(uint pivot);

    Task<uint> GetNextOrderNumberAsync();

    IQueryable<BasePurchasableEntity> GetVisibileEntities();

    Task UpdateOrderNoAsync(BasePurchasableEntity purchasableEntity);

    Task UpdatePurchasablesOrderAsync(List<BasePurchasableEntity> purchasableModels);
}