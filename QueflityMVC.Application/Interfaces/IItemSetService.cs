using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemSetService
    {
        ListItemSetsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex);
        string? GetItemSetVMForAdding();
    }
}
