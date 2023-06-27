using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IItemRepository : IBaseRepository<Item>
    {
        Item? GetItemWithIngredientsById(int itemId);

        void UpdateIngredients(int itemId, List<Ingredient> ingredients);
    }
}
