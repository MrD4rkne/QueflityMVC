using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.Interfaces;

public interface IItemService
{
    Task<Result> CreateItemAsync(ItemVm createItemVm);

    Task<Result> DeleteItemAsync(int id);

    Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm);

    Task<Result<ManageItemVm>> GetForEditAsync(int id);

    Task<Result<ManageItemVm>> GetItemVmForAddingAsync(int? categoryId);

    Task<Result<ItemVm>> UpdateItemAsync(ItemVm updateItemVm);

    Task<List<CategoryForSelectVm>> GetCategoriesForSelectVmAsync();

    Task<Result<ItemComponentsSelectionVm>> GetComponentsForSelectionVmAsync(int id);

    Task<Result> UpdateItemComponentsAsync(ItemComponentsSelectionVm selectionVm);
}