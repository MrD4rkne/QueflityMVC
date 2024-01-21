using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        public IQueryable<Ingredient> GetIngredientsForItem(int itemId);

        public IQueryable<Ingredient> GetIngredientsForPagination(int? itemId, string? nameFilter);
    }
}