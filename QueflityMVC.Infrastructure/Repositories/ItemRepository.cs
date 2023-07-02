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

        public override Item? Update(Item entityToUpdate)
        {
            if (entityToUpdate is null)
            {
                return default;
            }

            _dbContext.Items.Attach(entityToUpdate);
            _dbContext.Items.Entry(entityToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return GetById(entityToUpdate.Id);
        }

        public Item? GetItemWithIngredientsById(int itemId)
        {
            return _dbContext.Items.Include(x => x.Ingredients).Include(it => it.Image).FirstOrDefault(x => x.Id == itemId);
        }

        public void UpdateIngredients(int itemId, List<Ingredient> ingredients) {
            var item = GetItemWithIngredientsById(itemId);

            item.Ingredients.Clear();
            foreach(var ing in ingredients)
            {
                item.Ingredients.Add(ing);
            }
            Update(item);
        }
    }
}
