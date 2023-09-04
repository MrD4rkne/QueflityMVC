using AutoMapper;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public int CreateCategory(CategoryDTO createcategoryVM)
        {
            var categoryToCreate = _mapper.Map<Category>(createcategoryVM);

            return _repository.Add(categoryToCreate);
        }

        public void DeleteCategory(int id)
        {
            if (!_repository.CanDeleteCategory(id))
                throw new InvalidOperationException("First, remove or change category for items!");
            _repository.Delete(id);
        }

        public async Task<ListCategoriesVM> GetFilteredList(ListCategoriesVM listCategoriesVM)
        {
            if (listCategoriesVM is null)
            {
                throw new ArgumentNullException(nameof(listCategoriesVM));
            }

            IQueryable<Category> matchingCategories = _repository.GetFiltered(listCategoriesVM.NameFilter);

            listCategoriesVM.Pagination = await matchingCategories.Paginate<Category, CategoryForListVM>(listCategoriesVM.Pagination, _mapper.ConfigurationProvider);

            return listCategoriesVM;
        }

        public CategoryDTO GetVMForCreate()
        {
            CategoryDTO defaultCategoryDTO = new()
            {
                Id = default,
                Name = string.Empty
            };

            return defaultCategoryDTO;
        }

        public CategoryDTO? GetVMForEdit(int id)
        {
            var category = _repository.GetById(id);

            if (category is null)
                return null;

            return _mapper.Map<CategoryDTO>(category);
        }

        // Finish category updating
        public CategoryDTO UpdateCategory(CategoryDTO createcategoryVM)
        {
            var category = _mapper.Map<Category>(createcategoryVM);

            var updatedcategory = _repository.Update(category);

            return _mapper.Map<CategoryDTO>(updatedcategory);
        }
    }
}
