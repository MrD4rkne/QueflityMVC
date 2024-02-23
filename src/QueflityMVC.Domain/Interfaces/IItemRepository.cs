using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IItemRepository : IBaseRepository<Item>, IPurchasableRepository
{
    IQueryable<Item> GetFilteredItems(string? nameFilter = default, int? categoryId = default);

    Task<Item?> GetItemWithComponentsByIdAsync(int itemId);
    Task<bool> IsItemAPartOfAnyKitAsync(int id);
    Task UpdateComponentsAsync(int itemId, List<Component> components);
}