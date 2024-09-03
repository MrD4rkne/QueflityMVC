using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;

namespace QueflityMVC.Application.Interfaces;

public interface ICategoryService
{
    Task<Result> CreateCategoryAsync(CategoryVm createCategoryVm);

    Task DeleteCategoryAsync(int id);

    Task<ListCategoriesVm> GetFilteredListAsync(ListCategoriesVm listCategoriesVm);

    Task<Result<CategoryVm>> GetVmForEditAsync(int id);

    Task<Result> UpdateCategoryAsync(CategoryVm updateCategpryVm);
}