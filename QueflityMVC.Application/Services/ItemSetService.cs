using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.ItemSet;

namespace QueflityMVC.Application.Services
{
    public class ItemSetService : IItemSetService
    {
        public ListItemSetsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex)
        {
            throw new NotImplementedException();
        }

        public string? GetItemSetVMForAdding()
        {
            throw new NotImplementedException();
        }
    }
}
