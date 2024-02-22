using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IItemRepository : IBaseRepository<Item>
{
    IQueryable<Item> GetFilteredItems(string? nameFilter = default, int? categoryId = default);

    Task<Item?> GetItemWithIngredientsByIdAsync(int itemId);
    Task<bool> IsItemAPartOfAnyKitAsync(int id);
    Task UpdateIngredientsAsync(int itemId, List<Ingredient> ingredients);
}