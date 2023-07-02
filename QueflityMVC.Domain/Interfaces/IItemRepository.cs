﻿using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Item? GetItemWithIngredientsById(int itemId);

        void UpdateIngredients(int itemId, List<Ingredient> ingredients);
    }
}
