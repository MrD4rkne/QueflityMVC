using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Interfaces;

public interface IItemService
{
    Task<int> CreateItemAsync(ItemVm? createItemVm);

    Task<Result> DeleteItemAsync(int id);

    Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm);

    Task<ManageItemVm?> GetForEditAsync(int id);

    Task<Result<ManageItemVm>> GetItemVmForAddingAsync(int? categoryId);

    Task UpdateItemAsync(ItemVm? createItemVm);

    Task<List<CategoryForSelectVm>> GetCategoriesForSelectVmAsync();

    Task<Result<ItemComponentsSelectionVm>> GetComponentsForSelectionVmAsync(int id);

    Task UpdateItemComponentsAsync(ItemComponentsSelectionVm selectionVm);
}