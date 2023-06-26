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
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public override DbSet<Ingredient> Table() => _dbContext.Ingredients;

        public IQueryable<Ingredient> GetIngredientsForItem(int itemId)
        {
            return GetAll().Where(x => x.Items.Any(x => x.Id == itemId));
        }

        public IngredientRepository(Context dbContext) : base(dbContext) { }
    }
}
