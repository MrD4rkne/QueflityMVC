using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Interfaces;

public interface ICategoryService
{
    Task<int> CreateCategoryAsync(CategoryVm createCategoryVm);

    Task DeleteCategoryAsync(int id);

    Task<ListCategoriesVm> GetFilteredListAsync(ListCategoriesVm listCategoriesVm);

    Task<CategoryVm?> GetVmForEditAsync(int id);

    Task<CategoryVm> UpdateCategoryAsync(CategoryVm createCategoryVm);
}