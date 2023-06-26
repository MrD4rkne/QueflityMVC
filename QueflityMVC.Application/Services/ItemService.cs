using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Helpers;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemCategory;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class ItemService : IItemService
    {
        private IItemRepository _repository;
        private IItemCategoryRepository _categoryRepository;
        private IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IItemCategoryRepository categoryRepository)
        {
            _repository = itemRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<int> CreateItem(ItemDTO createItemVM, string itemsDirectory)
        {
            if (createItemVM == null)
                throw new ArgumentNullException("Viewmodel cannot be null!");

            if (createItemVM.Image == null || createItemVM.Image.FormFile == null)
                throw new ArgumentNullException("Image cannot be null!");

            createItemVM.Image!.FileUrl = await FileManager.UploadFile(itemsDirectory, createItemVM.Image.FormFile);

            var itemToCreate = _mapper.Map<Item>(createItemVM);

            return _repository.Add(itemToCreate);
        }

        public void DeleteItem(int id, string rootPath)
        {
            Item itemToDelete = _repository.GetById(id);
            if (itemToDelete == null)
                return;

            if(itemToDelete.Image!=null)
                FileManager.DeleteImage(rootPath, itemToDelete.Image?.FileUrl);
            // should automatically delete image (Cascade)
            _repository.Delete(id);
        }

        public ListItemsVM GetFilteredList(int itemCategoryId, string nameFilter, int pageSize, int pageIndex)
        {
            return GetListItemVM(_repository.GetAll().Where(x=>x.ItemCategoryId==itemCategoryId),nameFilter, pageSize, pageIndex);
        }

        public ListItemsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex)
        {
            return GetListItemVM(_repository.GetAll(), nameFilter, pageSize, pageIndex);
        }

        private ListItemsVM GetListItemVM(IQueryable<Item> itemsQuerable, string nameFilter, int pageSize, int pageIndex)
        {
            int itemsToSkip = (pageIndex - 1) * pageSize;

            ListItemsVM listItemVM = new()
            {
                NameFilter = nameFilter,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            var itemsToShow = itemsQuerable.Where(x => x.Name.Contains(nameFilter));
            listItemVM.TotalCount = itemsToShow.Count();
            itemsToShow = itemsToShow.Skip(itemsToSkip).Take(pageSize);

            listItemVM.Items = itemsToShow.ProjectTo<ItemForListVM>(_mapper.ConfigurationProvider).ToList();

            return listItemVM;
        }

        public CrEdItemVM? GetForEdit(int id)
        {
            var item = _repository.GetById(id);

            if (item is null)
                return null;

            CrEdItemVM crEdObjItemVM = new();
            crEdObjItemVM.ItemVM = _mapper.Map<ItemDTO>(item);
            crEdObjItemVM.ItemCategories = _categoryRepository.GetAll().ProjectTo<ItemCategoryForSelectVM>(_mapper.ConfigurationProvider).ToList();

            return crEdObjItemVM;
        }

        public async Task UpdateItem(ItemDTO updateItemVM, string rootPath)
        {
            var item = _mapper.Map<Item>(updateItemVM);

            if (item!=null && ShouldSwitchImages(updateItemVM))
            {
                if(item.Image!=null)
                    FileManager.DeleteImage(rootPath, item.Image.FileUrl);
                item.Image.FileUrl = await FileManager.UploadFile(rootPath, updateItemVM.Image.FormFile);
            }

            var updatedItem = _repository.Update(item);

        }

        public CrEdItemVM GetItemVMForAdding(int? categoryId)
        {
            CrEdItemVM crEdObjItem = new CrEdItemVM();

            crEdObjItem.ItemVM = new ItemDTO();
            if(categoryId.HasValue)
                crEdObjItem.ItemVM.CategoryId = categoryId.Value;

            crEdObjItem.ItemCategories = GetItemCategoriesForSelectVM();

            return crEdObjItem;
        }

        public List<ItemCategoryForSelectVM> GetItemCategoriesForSelectVM() { 
            return _categoryRepository.GetAll().ProjectTo<ItemCategoryForSelectVM>(_mapper.ConfigurationProvider).ToList();
        }

        private bool ShouldSwitchImages(ItemDTO updatedItem)
        {
            return updatedItem!=null && updatedItem.Image!=null && updatedItem.Image.FormFile!=null;
        }
    }
}
