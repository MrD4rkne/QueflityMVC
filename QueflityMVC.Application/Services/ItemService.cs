using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Item;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Ingredient;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

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

    public async Task<int> CreateItemAsync(ItemVM createItemVM)
    {
        createItemVM.Image!.FileUrl = await _fileService.UploadFileAsync(createItemVM.Image.FormFile);
        var itemToCreate = _mapper.Map<Item>(createItemVM);
        return await _itemRepository.AddAsync(itemToCreate);
    }

    public async Task<DeleteItemResult> DeleteItemAsync(int id)
    {
        Item? itemToDelete = await _itemRepository.GetByIdAsync(id);
        if(itemToDelete is null)
        {
            return DeleteItemResultsFactory.NotExist();
        }
        if(await _itemRepository.IsItemAPartOfAnyKitAsync(id))
        {
            return DeleteItemResultsFactory.ItemIsPartOfKit();
        }

        try
        {
            await _itemRepository.DeleteAsync(id);
        }
        catch(ResourceNotFoundException)
        {
            return DeleteItemResultsFactory.NotExist();
        }
        catch (Exception ex)
        {
            return DeleteItemResultsFactory.Exception(ex);
        }

        if (itemToDelete.Image is not null)
        {
            _fileService.DeleteImage(itemToDelete.Image!.FileUrl);
        }
        return DeleteItemResultsFactory.Success();
    }

    public async Task<ListItemsVM> GetFilteredListAsync(ListItemsVM listItemsVM)
    {
        IQueryable<Item> matchingItems = _itemRepository.GetFilteredItems(listItemsVM.NameFilter);
        listItemsVM.Pagination = await matchingItems.Paginate<Item, ItemForListVM>(listItemsVM.Pagination, _mapper.ConfigurationProvider);
        return listItemsVM;
    }

    public async Task<CrEdItemVM?> GetForEditAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();

        CrEdItemVM crEdObjItemVM = new()
        {
            ItemVM = _mapper.Map<ItemVM>(item),
            Categories = await _categoryRepository.GetAll().ProjectTo<CategoryForSelectVM>(_mapper.ConfigurationProvider).ToListAsync()
        };
        return crEdObjItemVM;
    }

    public async Task UpdateItemAsync(ItemVM updateItemVM)
    {
        var item = _mapper.Map<Item>(updateItemVM);

        if (ShouldSwitchImages(updateItemVM))
        {
            if (item.Image != null)
            {
                _fileService.DeleteImage(item.Image.FileUrl);
            }

            item.Image!.FileUrl = await _fileService.UploadFileAsync(updateItemVM.Image!.FormFile!);
        }
        _ = _itemRepository.UpdateAsync(item);
    }

    public async Task<CrEdItemVM> GetItemVMForAddingAsync(int? categoryId)
    {
        CrEdItemVM crEdObjItem = new CrEdItemVM()
        {
            Categories = await GetCategoriesForSelectVMAsync()
        };
        if(categoryId.HasValue)
        {
            crEdObjItem.ItemVM = new()
            {
                CategoryId = categoryId.Value
            };
        }
        return crEdObjItem;
    }

    public Task<List<CategoryForSelectVM>> GetCategoriesForSelectVMAsync()
    {
        return _categoryRepository.GetAll()
            .ProjectTo<CategoryForSelectVM>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    private bool ShouldSwitchImages(ItemVM updatedItem)
    {
        return updatedItem != null && updatedItem.Image != null && updatedItem.Image.FormFile != null;
    }

    public async Task<ItemIngredientsSelectionVM?> GetIngredientsForSelectionVMAsync(int id)
    {
        var item = await _itemRepository.GetItemWithIngredientsByIdAsync(id) ?? throw new EntityNotFoundException();

        var allIngredients = _ingredientRepository.GetAll();
        List<IngredientForSelection> allIngredientsVMs = await allIngredients.ProjectTo<IngredientForSelection>(_mapper.ConfigurationProvider)
            .ToListAsync();
        List<int> selectedIngredientsIds = item.Ingredients!
            .Select(x => x.Id)
            .ToList();
        ItemIngredientsSelectionVM selectionVM = new ItemIngredientsSelectionVM()
        {
            Item = _mapper.Map<ItemVM>(item),
            AllIngredients = allIngredientsVMs,
            SelectedIngredientsIds = selectedIngredientsIds
        };
        return selectionVM;
    }

    public Task UpdateItemIngredientsAsync(ItemIngredientsSelectionVM selectionVM)
    {
        var selectedIngredients = _mapper.Map<IEnumerable<Ingredient>>(selectionVM.AllIngredients.Where(x => x.IsSelected)).ToList();
        return _itemRepository.UpdateIngredientsAsync(selectionVM.Item.Id, selectedIngredients);
    }
}