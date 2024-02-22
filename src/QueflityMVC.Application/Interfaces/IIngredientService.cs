using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.Interfaces;

public interface IIngredientService
{
    Task<ListIngredientsVM> GetFilteredListAsync(ListIngredientsVM listIngredientsVM);

    Task<int> CreateIngredientAsync(IngredientVM ingredientToCreateVM);

    Task<IngredientVM?> GetIngredientVMForEditAsync(int id);

    Task UpdateIngredientAsync(IngredientVM ingredientToEditVM);

    Task DeleteIngredientAsync(int id);
}