using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Exceptions.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoriesRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _categoriesRepository = repository;
        _mapper = mapper;
    }

    public async Task<int> CreateCategoryAsync(CategoryVm createCategoryVm)
    {
        var categoryToCreate = _mapper.Map<Category>(createCategoryVm);
        return await _categoriesRepository.AddAsync(categoryToCreate);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        if (!await _categoriesRepository.IsAnyItemWithCategory(id))
            throw new InvalidOperationException("First, remove or change category for items!");
        await _categoriesRepository.DeleteAsync(id);
    }

    public async Task<ListCategoriesVm> GetFilteredListAsync(ListCategoriesVm listCategoriesVm)
    {
        var matchingCategories = _categoriesRepository.GetFiltered(listCategoriesVm.NameFilter);
        listCategoriesVm.Pagination =
            await matchingCategories.Paginate(listCategoriesVm.Pagination, _mapper.ConfigurationProvider);
        return listCategoriesVm;
    }

    public async Task<CategoryVm?> GetVmForEditAsync(int id)
    {
        var category = await _categoriesRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
        return _mapper.Map<CategoryVm>(category);
    }

    public async Task<CategoryVm> UpdateCategoryAsync(CategoryVm createCategoryVm)
    {
        var category = _mapper.Map<Category>(createCategoryVm);
        var updatedcategory = await _categoriesRepository.UpdateAsync(category);
        return _mapper.Map<CategoryVm>(updatedcategory);
    }
}