using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
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

    public async Task<int> CreateCategoryAsync(CategoryVM createcategoryVM)
    {
        var categoryToCreate = _mapper.Map<Category>(createcategoryVM);
        return await _categoriesRepository.AddAsync(categoryToCreate);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        if (!await _categoriesRepository.CanDeleteCategoryAsync(id))
            throw new InvalidOperationException("First, remove or change category for items!");
        await _categoriesRepository.DeleteAsync(id);
    }

    public async Task<ListCategoriesVM> GetFilteredListAsync(ListCategoriesVM listCategoriesVM)
    {
        IQueryable<Category> matchingCategories = _categoriesRepository.GetFiltered(listCategoriesVM.NameFilter);
        listCategoriesVM.Pagination = await matchingCategories.Paginate(listCategoriesVM.Pagination, _mapper.ConfigurationProvider);
        return listCategoriesVM;
    }

    public async Task<CategoryVM?> GetVMForEditAsync(int id)
    {
        var category = await _categoriesRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();
        return _mapper.Map<CategoryVM>(category);
    }

    public async Task<CategoryVM> UpdateCategoryAsync(CategoryVM createcategoryVM)
    {
        var category = _mapper.Map<Category>(createcategoryVM);
        var updatedcategory = await _categoriesRepository.UpdateAsync(category);
        return _mapper.Map<CategoryVM>(updatedcategory);
    }
}