﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Exceptions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class ItemService : IItemService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IComponentRepository _componentRepository;
    private readonly IFileService _fileService;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IPurchasableRepository _purchasableRepository;

    public ItemService(IItemRepository itemRepository, IMapper mapper, ICategoryRepository categoryRepository,
        IComponentRepository componentRepository, IFileService fileService,
        IPurchasableRepository purchasableRepository)
    {
        _itemRepository = itemRepository;
        _mapper = mapper;
        _categoryRepository = categoryRepository;
        _componentRepository = componentRepository;
        _fileService = fileService;
        _purchasableRepository = purchasableRepository;
    }

    public async Task<int> CreateItemAsync(ItemVm? createItemVm)
    {
        createItemVm.Image!.FileUrl = await _fileService.UploadFileAsync(createItemVm.Image.FormFile);
        var itemToCreate = _mapper.Map<Item>(createItemVm);
        itemToCreate.OrderNo = await _purchasableRepository.GetNextOrderNumberAsync();
        return await _itemRepository.AddAsync(itemToCreate);
    }

    public async Task<Result> DeleteItemAsync(int id)
    {
        var itemToDelete = await _itemRepository.GetByIdAsync(id);
        if (itemToDelete is null) return Result.Failure(Errors.Items.DoesNotExit);
        if (await _itemRepository.IsItemAPartOfAnyKitAsync(id)) return Result.Failure(Errors.Items.IsPartOfKit);

        try
        {
            await _itemRepository.DeleteAsync(id);
            _fileService.DeleteImage(itemToDelete.Image.FileUrl);
            if (itemToDelete.ShouldBeShown)
                await _purchasableRepository.BulkUpdateOrderAsync(itemToDelete.OrderNo.Value);
        }
        catch (ResourceNotFoundException)
        {
            return Result.Failure(Errors.Items.DoesNotExit);
        }

        if (itemToDelete.Image is not null) _fileService.DeleteImage(itemToDelete.Image!.FileUrl);
        return Result.Success();
    }

    public async Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm)
    {
        var matchingItems = _itemRepository.GetFilteredItems(listItemsVm.NameFilter, listItemsVm.CategoryId);
        listItemsVm.Pagination = await matchingItems.Paginate(listItemsVm.Pagination, _mapper.ConfigurationProvider);
        return listItemsVm;
    }

    public async Task<CrEdItemVm?> GetForEditAsync(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();

        CrEdItemVm crEdObjItemVm = new()
        {
            ItemVm = _mapper.Map<ItemVm>(item),
            Categories = await _categoryRepository.GetAll()
                .ProjectTo<CategoryForSelectVm>(_mapper.ConfigurationProvider).ToListAsync()
        };
        return crEdObjItemVm;
    }

    public async Task UpdateItemAsync(ItemVm? updateItemVm)
    {
        var item = _mapper.Map<Item>(updateItemVm);
        if (ShouldSwitchImages(updateItemVm))
        {
            if (item.Image != null) _fileService.DeleteImage(item.Image.FileUrl);
            item.Image!.FileUrl = await _fileService.UploadFileAsync(updateItemVm.Image!.FormFile!);
        }

        if (!item.ShouldBeShown)
        {
            item.OrderNo = null;
            await _purchasableRepository.BulkUpdateOrderAsync(item.OrderNo.Value);
        }

        if (item.ShouldBeShown && item.OrderNo is null)
            item.OrderNo = await _purchasableRepository.GetNextOrderNumberAsync();
        _ = await _itemRepository.UpdateAsync(item);
    }

    public async Task<Result<CrEdItemVm>> GetItemVmForAddingAsync(int? categoryId)
    {
        var crEdObjItem = new CrEdItemVm
        {
            Categories = await GetCategoriesForSelectVmAsync(),
            ItemVm = new ItemVm
            {
                CategoryId = categoryId
            }
        };

        if (crEdObjItem.Categories.Count == 0) return Result<CrEdItemVm>.Failure(Errors.Items.NoCategories);
        return Result<CrEdItemVm>.Success(crEdObjItem);
    }

    public Task<List<CategoryForSelectVm>> GetCategoriesForSelectVmAsync()
    {
        return _categoryRepository.GetAll()
            .ProjectTo<CategoryForSelectVm>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ItemComponentsSelectionVm?> GetComponentsForSelectionVmAsync(int id)
    {
        var item = await _itemRepository.GetItemWithComponentsByIdAsync(id) ?? throw new EntityNotFoundException();

        var allComponents = _componentRepository.GetAll();
        var allComponentsVMs = await allComponents.ProjectTo<ComponentForSelection>(_mapper.ConfigurationProvider)
            .ToListAsync();
        var selectedComponentsIds = item.Components!
            .Select(x => x.Id)
            .ToList();
        var selectionVm = new ItemComponentsSelectionVm
        {
            Item = _mapper.Map<ItemVm>(item),
            AllComponents = allComponentsVMs,
            SelectedComponentsIds = selectedComponentsIds
        };
        return selectionVm;
    }

    public Task UpdateItemComponentsAsync(ItemComponentsSelectionVm selectionVm)
    {
        var selectedComponents = _mapper.Map<IEnumerable<Component>>(selectionVm.AllComponents.Where(x => x.IsSelected))
            .ToList();
        return _itemRepository.UpdateComponentsAsync(selectionVm.Item.Id, selectedComponents);
    }

    private bool ShouldSwitchImages(ItemVm? updatedItem)
    {
        return updatedItem is not null && updatedItem.Image is not null && updatedItem.Image.FormFile != null;
    }
}