using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class CategoryService(ICategoryRepository repository, IMapper mapper) : ICategoryService
{
    public async Task<Result> CreateCategoryAsync(CategoryVm createCategoryVm)
    {
        if (await DoesCategoryWithNameExistAsync(createCategoryVm.Name))
        {
            return Result.Failure(Errors.Categories.DuplicatedName);
        }

        var categoryToCreate = mapper.Map<Category>(createCategoryVm);
        _ = await repository.AddAsync(categoryToCreate);

        return Result.Success();
    }

    public async Task<Result> DeleteCategoryAsync(int id)
    {
        if (!await repository.ExistsAsync(id))
        {
            return Result.Failure(Errors.Categories.DoesNotExist);
        }

        if (await repository.IsAnyItemWithCategory(id))
        {
            return Result.Failure(Errors.Categories.HasItems);
        }

        await repository.DeleteAsync(id);

        return Result.Success();
    }

    public async Task<ListCategoriesVm> GetFilteredListAsync(ListCategoriesVm listCategoriesVm)
    {
        var matchingCategories = repository.GetFiltered(listCategoriesVm.NameFilter)
            .OrderBy(category => category.Id);
        listCategoriesVm.Pagination =
            await matchingCategories.Paginate(listCategoriesVm.Pagination, mapper.ConfigurationProvider);
        return listCategoriesVm;
    }

    public async Task<Result<CategoryVm>> GetVmForEditAsync(int id)
    {
        var category = await repository.GetByIdAsync(id);

        if (category is null)
        {
            return Result<CategoryVm>.Failure(Errors.Categories.DoesNotExist);
        }

        var categoryVm = mapper.Map<CategoryVm>(category);
        return Result<CategoryVm>.Success(categoryVm);
    }

    public async Task<Result<CategoryVm>> UpdateCategoryAsync(CategoryVm updateCategoryVm)
    {
        if (!await repository.ExistsAsync(updateCategoryVm.Id))
        {
            return Result<CategoryVm>.Failure(Errors.Categories.DoesNotExist);
        }

        if (await DoesCategoryWithNameExistAsync(updateCategoryVm.Id, updateCategoryVm.Name))
        {
            return Result<CategoryVm>.Failure(Errors.Categories.DuplicatedName);
        }

        var category = mapper.Map<Category>(updateCategoryVm);
        var updatedCategory = await repository.UpdateAsync(category);

        var updatedCategoryVm = mapper.Map<CategoryVm>(updatedCategory);
        return Result<CategoryVm>.Success(updatedCategoryVm);
    }

    private Task<bool> DoesCategoryWithNameExistAsync(string name)
    {
        return repository.DoesCategoryWithNameExistAsync(name);
    }

    private Task<bool> DoesCategoryWithNameExistAsync(int id, string name)
    {
        return repository.DoesCategoryWithNameButNotIdExistAsync(id, name);
    }
}