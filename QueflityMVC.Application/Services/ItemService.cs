using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Helpers;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.ItemCategory;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IItemCategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;

        public ItemService(IItemRepository itemRepository, IMapper mapper, IItemCategoryRepository categoryRepository, IIngredientRepository ingredientRepository)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
        }

        public async Task<int> CreateItem(ItemDTO createItemVM, string contentRootPath)
        {
            if (createItemVM == null)
            {
                throw new ArgumentNullException("Viewmodel cannot be null!");
            }

            if (createItemVM.Image == null || createItemVM.Image.FormFile == null)
            {
                throw new ArgumentNullException("Image cannot be null!");
            }

            createItemVM.Image!.FileUrl = await FileManager.UploadFile(contentRootPath, createItemVM.Image.FormFile);

            var itemToCreate = _mapper.Map<Item>(createItemVM);

            return _itemRepository.Add(itemToCreate);
        }

        public void DeleteItem(int id, string contentRootPath)
        {
            Item? itemToDelete = _itemRepository.GetById(id);
            if (itemToDelete is null)
            {
                return;
            }

            if (itemToDelete.Image is not null)
            {
                FileManager.DeleteImage(contentRootPath, itemToDelete.Image!.FileUrl);
            }

            _itemRepository.Delete(id);
        }

        public ListItemsVM GetFilteredList(int itemCategoryId, string nameFilter, int pageSize, int pageIndex)
        {
            return GetListItemVM(_itemRepository.GetAll().Where(x => x.ItemCategoryId == itemCategoryId), nameFilter, pageSize, pageIndex);
        }

        public ListItemsVM GetFilteredList(string nameFilter, int pageSize, int pageIndex)
        {
            return GetListItemVM(_itemRepository.GetAll(), nameFilter, pageSize, pageIndex);
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
            var item = _itemRepository.GetById(id);

            if (item is null)
            {
                return null;
            }

            CrEdItemVM crEdObjItemVM = new();
            crEdObjItemVM.ItemVM = _mapper.Map<ItemDTO>(item);
            crEdObjItemVM.ItemCategories = _categoryRepository.GetAll().ProjectTo<ItemCategoryForSelectVM>(_mapper.ConfigurationProvider).ToList();

            return crEdObjItemVM;
        }

        public async Task UpdateItem(ItemDTO updateItemVM, string rootPath)
        {
            var item = _mapper.Map<Item>(updateItemVM);

            if (item != null && ShouldSwitchImages(updateItemVM))
            {
                if (item.Image != null)
                {
                    FileManager.DeleteImage(rootPath, item.Image.FileUrl);
                }

                item.Image!.FileUrl = await FileManager.UploadFile(rootPath, updateItemVM.Image!.FormFile!);
            }

            var updatedItem = _itemRepository.Update(item);
        }

        public CrEdItemVM GetItemVMForAdding(int? categoryId)
        {
            CrEdItemVM crEdObjItem = new CrEdItemVM();

            crEdObjItem.ItemVM = new ItemDTO();
            if (categoryId.HasValue)
            {
                crEdObjItem.ItemVM.CategoryId = categoryId.Value;
            }

            crEdObjItem.ItemCategories = GetItemCategoriesForSelectVM();

            return crEdObjItem;
        }

        public List<ItemCategoryForSelectVM> GetItemCategoriesForSelectVM()
        {
            return _categoryRepository.GetAll().ProjectTo<ItemCategoryForSelectVM>(_mapper.ConfigurationProvider).ToList();
        }

        private bool ShouldSwitchImages(ItemDTO updatedItem)
        {
            return updatedItem != null && updatedItem.Image != null && updatedItem.Image.FormFile != null;
        }

        public ItemIngredientsSelectionVM? GetIngredientsForSelectionVM(int id)
        {
            var item = _itemRepository.GetItemWithIngredientsById(id);
            if (item is null)
            {
                return null;
            }

            ItemIngredientsSelectionVM selectionVM = new ItemIngredientsSelectionVM()
            {
                Item = _mapper.Map<ItemDTO>(item)
            };

            var allIngredients = _ingredientRepository.GetAll();
            selectionVM.AllIngredients = allIngredients.ProjectTo<IngredientForSelection>(_mapper.ConfigurationProvider).ToList();

            selectionVM.SelectedIngredients = item.Ingredients.Select(x => x.Id).ToList();

            return selectionVM;
        }

        public void UpdateItemIngredients(ItemIngredientsSelectionVM selectionVM)
        {
            if (selectionVM is null)
            {
                throw new ArgumentNullException("View model cannot be null");
            }
            if (selectionVM.AllIngredients is null)
            {
                throw new ArgumentNullException("All ingredients cannot be null");
            }
            if (selectionVM.Item is null)
            {
                throw new ArgumentNullException("Item cannot be null");
            }

            var selectedIngredients = _mapper.Map<IEnumerable<Ingredient>>(selectionVM.AllIngredients.Where(x => x.IsSelected)).ToList();

            _itemRepository.UpdateIngredients(selectionVM.Item.Id, selectedIngredients);
        }
    }
}
