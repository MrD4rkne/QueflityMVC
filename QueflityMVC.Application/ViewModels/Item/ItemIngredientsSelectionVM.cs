using QueflityMVC.Application.ViewModels.Ingredient;

namespace QueflityMVC.Application.ViewModels.Item
{
    public class ItemIngredientsSelectionVM
    {
        public ItemDTO Item { get; set; }

        public List<int> SelectedIngredients { get; set; }

        public List<IngredientForSelection> AllIngredients { get; set; }
    }
}
