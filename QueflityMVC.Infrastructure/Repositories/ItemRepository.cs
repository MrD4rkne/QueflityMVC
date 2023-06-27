using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemRepository : BaseRepository<Item>, IItemRepository
    {
        public override DbSet<Item> Table() => _dbContext.Items;

        public ItemRepository(Context dbContext) : base(dbContext) { }

        public override Item? GetById(int entityId)
        {
            return Table().Include(it => it.Image).FirstOrDefault(it => it.Id == entityId);
        }

        public override Item? Update(Item entityToUpdate)
        {
            if (entityToUpdate is null)
                return default;

            Table().Attach(entityToUpdate);
            Table().Entry(entityToUpdate).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return GetById(entityToUpdate.Id);
        }

        public Item? GetItemWithIngredientsById(int itemId)
        {
            return Table().Include(x => x.Ingredients).Include(it => it.Image).FirstOrDefault(x => x.Id == itemId);
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
