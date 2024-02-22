using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Exceptions.UseCases;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Item;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class ItemService : IItemService
{
    private readonly IItemRepository _itemRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public ItemService(IItemRepository itemRepository, IMapper mapper, ICategoryRepository categoryRepository, IComponentRepository componentRepository, IFileService fileService)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _componentRepository = componentRepository;
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
        IQueryable<Item> matchingItems = _itemRepository.GetFilteredItems(listItemsVM.NameFilter, listItemsVM.CategoryId);
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
        if(crEdObjItem.Categories.Count == 0)
        {
            throw new NoCategoriesException();
        }

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

    public async Task<ItemComponentsSelectionVM?> GetComponentsForSelectionVMAsync(int id)
    {
        var item = await _itemRepository.GetItemWithComponentsByIdAsync(id) ?? throw new EntityNotFoundException();

        var allComponents = _componentRepository.GetAll();
        List<ComponentForSelection> allComponentsVMs = await allComponents.ProjectTo<ComponentForSelection>(_mapper.ConfigurationProvider)
            .ToListAsync();
        List<int> selectedComponentsIds = item.Components!
            .Select(x => x.Id)
            .ToList();
        ItemComponentsSelectionVM selectionVM = new ItemComponentsSelectionVM()
        {
            Item = _mapper.Map<ItemVM>(item),
            AllComponents = allComponentsVMs,
            SelectedComponentsIds = selectedComponentsIds
        };
        return selectionVM;
    }

    public Task UpdateItemComponentsAsync(ItemComponentsSelectionVM selectionVM)
    {
        var selectedComponents = _mapper.Map<IEnumerable<Component>>(selectionVM.AllComponents.Where(x => x.IsSelected)).ToList();
        return _itemRepository.UpdateComponentsAsync(selectionVM.Item.Id, selectedComponents);
    }
}