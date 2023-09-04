using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public ItemRepository(Context dbContext) : base(dbContext) { }

        public override Item? GetById(int entityId)
        {
            return _dbContext.Items.Include(it => it.Image).FirstOrDefault(it => it.Id == entityId);
        }

        public IQueryable<Item> GetFilteredItems(string? nameFilter)
        {
            if (!string.IsNullOrEmpty(nameFilter))
            {
                return GetAll().Where(ct => ct.Name.StartsWith(nameFilter));
            }
            else
            {
                return GetAll();
            }
        }

        public Item? GetItemWithIngredientsById(int itemId)
        {
            return _dbContext.Items.Include(x => x.Ingredients).Include(it => it.Image).FirstOrDefault(x => x.Id == itemId);
        }

        public void UpdateIngredients(int itemId, List<Ingredient> ingredients)
        {
            var item = GetItemWithIngredientsById(itemId);
            if (item is null)
            {
                throw new NullReferenceException();
            }

            if (ingredients is not null)
            {
                item.Ingredients = ingredients;
            }

            Update(item);
        }
    }
}
