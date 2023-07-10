using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.Interfaces
{
    public interface IIngredientService
    {
        ListIngredientsVM GetFilteredList(int? itemId, string nameFilter, int pageSize, int pageIndex);

        int CreateIngredient(IngredientDTO ingredientToCreateDTO);

        IngredientDTO? GetIngredientVMForEdit(int id);

        void UpdateIngredient(IngredientDTO ingredientToEditDTO);

        void DeleteIngredient(int id);
    }
}
