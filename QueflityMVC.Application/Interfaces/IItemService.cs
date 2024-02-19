using QueflityMVC.Application.Results.Item;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Interfaces;

public interface IItemService
{
    Task<int> CreateItemAsync(ItemVM createItemVM);

    Task<DeleteItemResult> DeleteItemAsync(int id);

    Task<ListItemsVM> GetFilteredListAsync(ListItemsVM listItemsVM);

    Task<CrEdItemVM?> GetForEditAsync(int id);

    Task<CrEdItemVM> GetItemVMForAddingAsync(int? categoryId);

    Task UpdateItemAsync(ItemVM createItemVM);

    Task<List<CategoryForSelectVM>> GetCategoriesForSelectVMAsync();

    Task<ItemIngredientsSelectionVM?> GetIngredientsForSelectionVMAsync(int id);

    Task UpdateItemIngredientsAsync(ItemIngredientsSelectionVM selectionVM);
}