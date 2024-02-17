using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Interfaces;

public interface ICategoryService
{
    Task<int> CreateCategoryAsync(CategoryVM createcategoryVM);

    Task DeleteCategoryAsync(int id);

    Task<ListCategoriesVM> GetFilteredListAsync(ListCategoriesVM listCategoriesVM);

    Task<CategoryVM?> GetVMForEditAsync(int id);

    Task<CategoryVM> UpdateCategoryAsync(CategoryVM createcategoryVM);
}