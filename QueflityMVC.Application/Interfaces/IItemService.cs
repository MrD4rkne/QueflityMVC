using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemService
    {
        Task<int> CreateItem(ItemDTO createItemVM, string contentRootPath);

        void DeleteItem(int id, string contentRootPath);

        Task<ListItemsVM> GetFilteredList(ListItemsVM listItemsVM);

        CrEdItemVM? GetForEdit(int id);

        CrEdItemVM GetItemVMForAdding(int? categoryId);

        Task UpdateItem(ItemDTO createItemVM, string rootPath);

        List<CategoryForSelectVM> GetCategoriesForSelectVM();

        ItemIngredientsSelectionVM? GetIngredientsForSelectionVM(int id);

        void UpdateItemIngredients(ItemIngredientsSelectionVM selectionVM);
    }
}