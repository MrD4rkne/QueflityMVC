using QueflityMVC.Application.Results.Item;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Interfaces;

public interface IItemService
{
    Task<int> CreateItemAsync(ItemVm? createItemVm);

    Task<DeleteItemResult> DeleteItemAsync(int id);

    Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm);

    Task<CrEdItemVm?> GetForEditAsync(int id);

    Task<CrEdItemVm> GetItemVmForAddingAsync(int? categoryId);

    Task UpdateItemAsync(ItemVm? createItemVm);

    Task<List<CategoryForSelectVm>> GetCategoriesForSelectVmAsync();

    Task<ItemComponentsSelectionVm?> GetComponentsForSelectionVmAsync(int id);

    Task UpdateItemComponentsAsync(ItemComponentsSelectionVm selectionVm);
}