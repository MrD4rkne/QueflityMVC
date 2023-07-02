using QueflityMVC.Application.ViewModels.ItemCategory;

namespace QueflityMVC.Application.Interfaces
{
    public interface IItemCategoryService
    {
        int CreateItemCategory(ItemCategoryDTO createItemCategoryVM);

        void DeleteItemCategory(int id);

        ListItemCategoriesVM GetFilteredList(string nameFilter, int pageSize, int pageIndex);

        ItemCategoryDTO? GetVMForEdit(int id);

        void UpdateItemCategory(ItemCategoryDTO createItemCategoryVM);

    }
}
