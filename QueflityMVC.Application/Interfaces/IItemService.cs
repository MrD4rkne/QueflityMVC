using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemService
    {
        Task<int> CreateItem(ItemDTO createItemVM, string imagesDirectory);
        void DeleteItem(int id, string rootPath);
        ListItemsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex);
        ListItemsVM GetFilteredList(int itemCategoryId, string nameFilter, int pageSize, int pageIndex);
        CrEdItemVM? GetForEdit(int id);
        CrEdItemVM GetItemVMForAdding(int? categoryId);
        Task UpdateItem(ItemDTO createItemVM, string rootPath);

        List<ItemCategoryForSelectVM> GetItemCategoriesForSelectVM();
    }
}
