using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QueflityMVC.Application.Common.Pagination;
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
    IProductRepository purchasableRepository,
    ILogger<ItemService> logger)
    : IItemService
{
    public async Task<Result> CreateItemAsync(ItemVm createItemVm)
    {
        if (!await categoryRepository.ExistsAsync(createItemVm.CategoryId.Value))
        {
            return Result.Failure(Errors.Categories.DoesNotExist);
        }

        try
        {
            createItemVm.Image!.FileUrl = await fileService.UploadFileAsync(createItemVm.Image.FormFile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Image upload failed when creating item: {Item}", createItemVm);
            return Result.Failure(Errors.Files.FileUploadFailed);
        }

        var itemToCreate = mapper.Map<Item>(createItemVm);

        if (itemToCreate.ShouldBeShown)
        {
            itemToCreate.OrderNo = await purchasableRepository.GetNextOrderNumberAsync();
        }

        await itemRepository.AddAsync(itemToCreate);

        return Result.Success();
    }

    public async Task<Result> DeleteItemAsync(int id)
    {
        var itemToDelete = await itemRepository.GetByIdAsync(id);
        if (itemToDelete is null)
        {
            return Result.Failure(Errors.Items.DoesNotExit);
        }

        if (await itemRepository.IsItemAPartOfAnyKitAsync(id))
        {
            return Result.Failure(Errors.Items.IsPartOfKit);
        }

        try
        {
            await itemRepository.DeleteAsync(id);
            if (itemToDelete.ShouldBeShown)
            {
                await itemRepository.BulkUpdateOrderAsync(itemToDelete.OrderNo.Value);
            }
        }
        catch (ResourceNotFoundException)
        {
            return Result.Failure(Errors.Items.DoesNotExit);
        }

        if (itemToDelete.Image is not null)
        {
            fileService.DeleteImage(itemToDelete.Image!.FileUrl);
        }

        return Result.Success();
    }

    public async Task<ListItemsVm> GetFilteredListAsync(ListItemsVm listItemsVm)
    {
        var matchingItems = itemRepository.GetFilteredItems(listItemsVm.NameFilter, listItemsVm.CategoryId);
        matchingItems = matchingItems.OrderBy(item => item.Id);

        listItemsVm.Pagination = await matchingItems.Paginate(listItemsVm.Pagination, mapper.ConfigurationProvider);
        return listItemsVm;
    }

    public async Task<Result<ManageItemVm>> GetForEditAsync(int id)
    {
        var item = await itemRepository.GetByIdAsync(id);
        if (item is null)
        {
            return Result<ManageItemVm>.Failure(Errors.Items.DoesNotExit);
        }

        var categories = await categoryRepository.GetAll()
            .ProjectTo<CategoryForSelectVm>(mapper.ConfigurationProvider)
            .ToListAsync();

        ManageItemVm manageObjItemVm = new()
        {
            ItemVm = mapper.Map<ItemVm>(item),
            Categories = categories
        };
        return Result<ManageItemVm>.Success(manageObjItemVm);
    }

    public async Task<Result<ItemVm>> UpdateItemAsync(ItemVm updateItemVm)
    {
        var itemToUpdate = await itemRepository.GetByIdAsync(updateItemVm.Id);
        if (itemToUpdate is null)
        {
            return Result<ItemVm>.Failure(Errors.Items.DoesNotExit);
        }

        if (!await categoryRepository.ExistsAsync(updateItemVm.CategoryId.Value))
        {
            return Result<ItemVm>.Failure(Errors.Categories.DoesNotExist);
        }

        itemToUpdate.CategoryId = updateItemVm.CategoryId.Value;
        itemToUpdate.Name = updateItemVm.Name;
        itemToUpdate.SetPrice(updateItemVm.Price);
        itemToUpdate.Image.AltDescription = updateItemVm.Image.AltDescription;
        itemToUpdate.ShouldBeShown = updateItemVm.ShouldBeShown;

        if (ShouldSwitchImages(updateItemVm))
        {
            try
            {
                if (itemToUpdate.Image is not null)
                {
                    fileService.DeleteImage(itemToUpdate.Image.FileUrl);
                }

                itemToUpdate.Image.FileUrl = await fileService.UploadFileAsync(updateItemVm.Image.FormFile);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Image upload failed when updating item: {Item}", updateItemVm);
                return Result<ItemVm>.Failure(Errors.Files.FileUploadFailed);
            }
        }

        // If the item was not shown before, and now it should be, get the next order number
        if (itemToUpdate is { ShouldBeShown: true, OrderNo: null })
        {
            itemToUpdate.OrderNo = await purchasableRepository.GetNextOrderNumberAsync();
        }

        // If the item was shown before, and now it should not be, remove the order number
        if (itemToUpdate is { ShouldBeShown: false, OrderNo: not null })
        {
            await itemRepository.BulkUpdateOrderAsync(itemToUpdate.OrderNo.Value);
            itemToUpdate.OrderNo = null;
        }

        itemToUpdate = await itemRepository.UpdateAsync(itemToUpdate);
        var updatedVm = mapper.Map<ItemVm>(itemToUpdate);
        return Result<ItemVm>.Success(updatedVm);
    }

    public async Task<Result<ManageItemVm>> GetItemVmForAddingAsync(int? categoryId)
    {
        var crEdObjItem = new ManageItemVm
        {
            Categories = await GetCategoriesForSelectVmAsync(),
            ItemVm = new ItemVm
            {
                CategoryId = categoryId
            }
        };

        if (crEdObjItem.Categories.Count == 0)
        {
            return Result<ManageItemVm>.Failure(Errors.Items.NoCategories);
        }

        return Result<ManageItemVm>.Success(crEdObjItem);
    }

    public Task<List<CategoryForSelectVm>> GetCategoriesForSelectVmAsync()
    {
        return categoryRepository.GetAll()
            .OrderBy(c => c.Name)
            .ProjectTo<CategoryForSelectVm>(mapper.ConfigurationProvider)
            .ToListAsync();
    }

    public async Task<Result<ItemComponentsSelectionVm>> GetComponentsForSelectionVmAsync(int id)
    {
        var item = await itemRepository.GetItemWithComponentsByIdAsync(id);
        if (item is null)
        {
            return Result<ItemComponentsSelectionVm>.Failure(Errors.Items.DoesNotExit);
        }

        var allComponents = componentRepository.GetAll()
            .OrderBy(component => component.Id);

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
        return Result<ItemComponentsSelectionVm>.Success(selectionVm);
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