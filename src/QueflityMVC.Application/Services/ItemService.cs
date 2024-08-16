using AutoMapper;
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

public class ItemService(
    IItemRepository itemRepository,
    IMapper mapper,
    ICategoryRepository categoryRepository,
    IComponentRepository componentRepository,
    IFileService fileService,
    IProductRepository purchasableRepository)
    : IItemService
{
    public async Task<int> CreateItemAsync(ItemVm? createItemVm)
    {
        createItemVm.Image!.FileUrl = await fileService.UploadFileAsync(createItemVm.Image.FormFile);
        var itemToCreate = mapper.Map<Item>(createItemVm);
        itemToCreate.OrderNo = await purchasableRepository.GetNextOrderNumberAsync();
        return await itemRepository.AddAsync(itemToCreate);
    }

    public async Task<Result> DeleteItemAsync(int id)
    {
        var itemToDelete = await itemRepository.GetByIdAsync(id);
        if (itemToDelete is null) return Result.Failure(Errors.Items.DoesNotExit);
        if (await itemRepository.IsItemAPartOfAnyKitAsync(id)) return Result.Failure(Errors.Items.IsPartOfKit);

        try
        {
            await itemRepository.DeleteAsync(id);
            if (itemToDelete.ShouldBeShown) await itemRepository.BulkUpdateOrderAsync(itemToDelete.OrderNo.Value);
        }
        catch (ResourceNotFoundException)
        {
            return Result.Failure(Errors.Items.DoesNotExit);
        }

        if (itemToDelete.Image is not null) fileService.DeleteImage(itemToDelete.Image!.FileUrl);
        return Result.Success();
    }

    public async Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm)
    {
        var matchingItems = itemRepository.GetFilteredItems(listItemsVm.NameFilter, listItemsVm.CategoryId);
        listItemsVm.Pagination = await matchingItems.Paginate(listItemsVm.Pagination, mapper.ConfigurationProvider);
        return listItemsVm;
    }

    public async Task<CrEdItemVm?> GetForEditAsync(int id)
    {
        var item = await itemRepository.GetByIdAsync(id) ?? throw new EntityNotFoundException();

        CrEdItemVm crEdObjItemVm = new()
        {
            ItemVm = mapper.Map<ItemVm>(item),
            Categories = await categoryRepository.GetAll()
                .ProjectTo<CategoryForSelectVm>(mapper.ConfigurationProvider).ToListAsync()
        };
        return crEdObjItemVm;
    }

    public async Task UpdateItemAsync(ItemVm? updateItemVm)
    {
        var item = mapper.Map<Item>(updateItemVm);
        if (ShouldSwitchImages(updateItemVm))
        {
            if (item.Image != null) fileService.DeleteImage(item.Image.FileUrl);
            item.Image!.FileUrl = await fileService.UploadFileAsync(updateItemVm.Image!.FormFile!);
        }

        item.OrderNo = await itemRepository.GetOrderNoByIdAsync(item.Id);

        if (item.ShouldBeShown && item.OrderNo is null)
            item.OrderNo = await purchasableRepository.GetNextOrderNumberAsync();
        _ = await itemRepository.UpdateAsync(item);
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
        return categoryRepository.GetAll()
            .ProjectTo<CategoryForSelectVm>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<ItemComponentsSelectionVm?> GetComponentsForSelectionVmAsync(int id)
    {
        var item = await itemRepository.GetItemWithComponentsByIdAsync(id) ?? throw new EntityNotFoundException();

        var allComponents = componentRepository.GetAll();
        var allComponentsVMs = await allComponents.ProjectTo<ComponentForSelection>(mapper.ConfigurationProvider)
            .ToListAsync();
        var selectedComponentsIds = item.Components!
            .Select(x => x.Id)
            .ToList();
        var selectionVm = new ItemComponentsSelectionVm
        {
            Item = mapper.Map<ItemVm>(item),
            AllComponents = allComponentsVMs,
            SelectedComponentsIds = selectedComponentsIds
        };
        return selectionVm;
    }

    public Task UpdateItemComponentsAsync(ItemComponentsSelectionVm selectionVm)
    {
        var selectedComponents = mapper.Map<IEnumerable<Component>>(selectionVm.AllComponents.Where(x => x.IsSelected))
            .ToList();
        return itemRepository.UpdateComponentsAsync(selectionVm.Item.Id, selectedComponents);
    }

    private bool ShouldSwitchImages(ItemVm? updatedItem)
    {
        return updatedItem is not null && updatedItem.Image is not null && updatedItem.Image.FormFile != null;
    }
}