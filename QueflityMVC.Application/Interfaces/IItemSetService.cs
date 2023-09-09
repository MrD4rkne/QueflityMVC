using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemSet;
using QueflityMVC.Application.ViewModels.SetElement;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemSetService
    {
        Task<int> CreateItemSet(ItemSetDTO ItemSetDTO, string contentRootPath);
        Task<int> EditItemSet(ItemSetDTO editItemSetDTO, string contentRootPath);
        ItemSetDetailsVM GetDetailsVM(int id);
        Task<ListItemSetsVM> GetFilteredList(ListItemSetsVM listItemSetsVM);
        Task<ListItemsForComponentsVM> GetFilteredListForComponents(int id);
        Task<ListItemsForComponentsVM> GetFilteredListForComponents(ListItemsForComponentsVM listItemsForComponentsVM);
        ItemSetDTO GetItemSetVMForAdding();
        ItemSetDTO GetItemSetVMForEdit(int id);
        ElementDTO GetVMForAddingComponent(int setId, int itemId);
    }
}
