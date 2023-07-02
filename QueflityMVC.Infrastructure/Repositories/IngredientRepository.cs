using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(Context dbContext) : base(dbContext) { }

        public IQueryable<Ingredient> GetIngredientsForItem(int itemId)
        {
            return GetAll().Where(x => x.Items.Any(x => x.Id == itemId));
        }
    }
}
