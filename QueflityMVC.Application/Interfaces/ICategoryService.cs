using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Interfaces
{
    public interface ICategoryService
    {
        int CreateCategory(CategoryDTO createcategoryVM);

        void DeleteCategory(int id);

        Task<ListCategoriesVM> GetFilteredList(ListCategoriesVM listCategoriesVM);

        CategoryDTO GetVMForCreate();

        CategoryDTO? GetVMForEdit(int id);

        CategoryDTO UpdateCategory(CategoryDTO createcategoryVM);
    }
}