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
            return GetAll().Where(x => x.Items!.Any(x => x.Id == itemId));
        }

        public IQueryable<Ingredient> GetIngredientsForPagination(int? itemId, string? nameFilter)
        {
            IQueryable<Ingredient> matchingIngredients = GetAll();

            if (itemId.HasValue)
            {
                matchingIngredients = matchingIngredients.Where(x => x.Items!.Any(y => y.Id == itemId));
            }
            if (!string.IsNullOrEmpty(nameFilter))
            {
                matchingIngredients = matchingIngredients.Where(x => x.Name.StartsWith(nameFilter));
            }

            return matchingIngredients;
        }
    }
}
