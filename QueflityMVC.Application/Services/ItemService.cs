using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ItemService(IItemRepository itemRepository, IMapper mapper, ICategoryRepository categoryRepository, IIngredientRepository ingredientRepository, IFileService fileService)
        {
            _itemRepository = itemRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
            _ingredientRepository = ingredientRepository;
            _fileService = fileService;
        }

        public async Task<int> CreateItem(ItemDTO createItemVM, string contentRootPath)
        {
            createItemVM.Image!.FileUrl = await _fileService.UploadFile(contentRootPath, createItemVM.Image.FormFile);

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
                _fileService.DeleteImage(contentRootPath, itemToDelete.Image!.FileUrl);
            }
            _itemRepository.Delete(id);
        }

        public async Task<ListItemsVM> GetFilteredList(ListItemsVM listItemsVM)
        {
            IQueryable<Item> matchingItems = _itemRepository.GetFilteredItems(listItemsVM.NameFilter);
            listItemsVM.Pagination = await matchingItems.Paginate<Item, ItemForListVM>(listItemsVM.Pagination, _mapper.ConfigurationProvider);

            return listItemsVM;
        }

        public CrEdItemVM? GetForEdit(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item is null)
            {
                return null;
            }

            CrEdItemVM crEdObjItemVM = new()
            {
                ItemVM = _mapper.Map<ItemDTO>(item),
                Categories = _categoryRepository.GetAll().ProjectTo<CategoryForSelectVM>(_mapper.ConfigurationProvider).ToList()
            };

            return crEdObjItemVM;
        }

        public async Task UpdateItem(ItemDTO updateItemVM, string rootPath)
        {
            var item = _mapper.Map<Item>(updateItemVM);

            if (item is null)
            {
                return;
            }

            if (ShouldSwitchImages(updateItemVM))
            {
                if (item.Image != null)
                {
                    _fileService.DeleteImage(rootPath, item.Image.FileUrl);
                }

                item.Image!.FileUrl = await _fileService.UploadFile(rootPath, updateItemVM.Image!.FormFile!);
            }
            _ = _itemRepository.Update(item);
        }

        public CrEdItemVM GetItemVMForAdding(int? categoryId)
        {
            var avaiableCategories = GetCategoriesForSelectVM();

            CrEdItemVM crEdObjItem = new CrEdItemVM()
            {
                ItemVM = new(),
                Categories = avaiableCategories
            };

            return crEdObjItem;
        }

        public List<CategoryForSelectVM> GetCategoriesForSelectVM()
        {
            return _categoryRepository.GetAll().ProjectTo<CategoryForSelectVM>(_mapper.ConfigurationProvider).ToList();
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

            var allIngredients = _ingredientRepository.GetAll();
            List<IngredientForSelection> allIngredientsDTOs = allIngredients.ProjectTo<IngredientForSelection>(_mapper.ConfigurationProvider).ToList();
            List<int> selectedIngredientsIds = item.Ingredients!.Select(x => x.Id).ToList();

            ItemIngredientsSelectionVM selectionVM = new ItemIngredientsSelectionVM()
            {
                Item = _mapper.Map<ItemDTO>(item),
                AllIngredients = allIngredientsDTOs,
                SelectedIngredientsIds = selectedIngredientsIds
            };

            return selectionVM;
        }

        public void UpdateItemIngredients(ItemIngredientsSelectionVM selectionVM)
        {
            var selectedIngredients = _mapper.Map<IEnumerable<Ingredient>>(selectionVM.AllIngredients.Where(x => x.IsSelected)).ToList();
            _itemRepository.UpdateIngredients(selectionVM.Item.Id, selectedIngredients);
        }
    }
}
