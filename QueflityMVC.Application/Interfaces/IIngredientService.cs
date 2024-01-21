using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.Interfaces
{
    public interface IIngredientService
    {
        Task<ListIngredientsVM> GetFilteredList(ListIngredientsVM listIngredientsVM);

        int CreateIngredient(IngredientDTO ingredientToCreateDTO);

        IngredientDTO? GetIngredientVMForEdit(int id);

        void UpdateIngredient(IngredientDTO ingredientToEditDTO);

        void DeleteIngredient(int id);
    }
}