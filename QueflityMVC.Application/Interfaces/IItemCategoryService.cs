using QueflityMVC.Application.ViewModels.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemCategoryService
    {
        int CreateItemCategory(ItemCategoryDTO createItemCategoryVM);
        void DeleteItemCategory(int id);
        ListItemCategoryVM GetFilteredList(string nameFilter, int pageSize, int pageIndex);
        ItemCategoryDTO? GetForEdit(int id);
        int UpdateItemCategory(ItemCategoryDTO createItemCategoryVM);
    }
}
