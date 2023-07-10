using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.ItemCategory;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class ItemCategoryService : IItemCategoryService
    {
        private readonly IItemCategoryRepository _repository;
        private readonly IMapper _mapper;

        public ItemCategoryService(IItemCategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public int CreateItemCategory(ItemCategoryDTO createItemCategoryVM)
        {
            var itemCategoryToCreate = _mapper.Map<ItemCategory>(createItemCategoryVM);

            return _repository.Add(itemCategoryToCreate);
        }

        public void DeleteItemCategory(int id)
        {
            if (!_repository.CanDeleteItemCategory(id))
                throw new InvalidOperationException("First, remove or change category for items!");
            _repository.Delete(id);
        }

        public ListItemCategoriesVM GetFilteredList(string nameFilter, int pageSize, int pageIndex)
        {
            int itemsToSkip = (pageIndex - 1) * pageSize;

            ListItemCategoriesVM listItemCategoryVM = new()
            {
                NameFilter = nameFilter,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var itemsToShow = _repository.GetAll().Where(x => x.Name.Contains(nameFilter));
            listItemCategoryVM.TotalCount = itemsToShow.Count();
            itemsToShow = itemsToShow.Skip(itemsToSkip).Take(pageSize);

            listItemCategoryVM.ItemCategories = itemsToShow.ProjectTo<ItemCategoryForListVM>(_mapper.ConfigurationProvider).ToList();

            return listItemCategoryVM;
        }

        public ItemCategoryDTO? GetVMForEdit(int id)
        {
            var itemCategory = _repository.GetById(id);

            if (itemCategory is null)
                return null;

            return _mapper.Map<ItemCategoryDTO>(itemCategory);
        }

        public void UpdateItemCategory(ItemCategoryDTO createItemCategoryVM)
        {
            var itemCategory = _mapper.Map<ItemCategory>(createItemCategoryVM);

            var updatedItemCategory = _repository.Update(itemCategory);
        }
    }
}
