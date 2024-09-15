using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Exceptions;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class KitService(
    IKitRepository kitRepository,
    IItemRepository itemRepository,
    IMapper mapper,
    IFileService fileService,
    IProductRepository productRepository,
    ILogger<KitService> logger)
    : IKitService
{
    public async Task<Result<KitVm>> CreateKitAsync(KitVm kitVm)
    {
        try
        {
            kitVm.Image!.FileUrl = await fileService.UploadFileAsync(kitVm.Image.FormFile);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Image upload failed when creating kit: {Kit}", kitVm);
            return Result<KitVm>.Failure(Errors.Files.FileUploadFailed);
        }

        var kitToCreate = mapper.Map<Kit>(kitVm);
        
        if (kitToCreate.ShouldBeShown)
        {
            kitToCreate.OrderNo = await productRepository.GetNextOrderNumberAsync();
        }
        
        var kit = await kitRepository.AddAsync(kitToCreate);
        return Result<KitVm>.Success(mapper.Map<KitVm>(kit));
    }

    public async Task<Result<KitVm>> EditKitAsync(KitVm editKitVm)
    {
        var kitToUpdate = await kitRepository.GetByIdAsync(editKitVm.Id);
        if (kitToUpdate is null)
        {
            return Result<KitVm>.Failure(Errors.Kits.DoesNotExit);
        }

        if (ShouldSwitchImages(editKitVm.Image))
        {
            try
            {
                string newUrl = await fileService.UploadFileAsync(editKitVm.Image.FormFile);
                
                if (kitToUpdate.Image is not null)
                {
                    fileService.DeleteImage(kitToUpdate.Image.FileUrl);
                }
                
                kitToUpdate.Image.FileUrl = newUrl;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Image upload failed when updating kit: {Kit}", editKitVm);
                return Result<KitVm>.Failure(Errors.Files.FileUploadFailed);
            }
        }
        
        kitToUpdate.Name = editKitVm.Name;
        kitToUpdate.Description = editKitVm.Description;
        kitToUpdate.Image.AltDescription = editKitVm.Image.AltDescription;
        kitToUpdate.ShouldBeShown = editKitVm.ShouldBeShown;

        if (kitToUpdate is {ShouldBeShown: true, OrderNo: null})
        {
            kitToUpdate.OrderNo = await productRepository.GetNextOrderNumberAsync();
        }

        if (kitToUpdate is { ShouldBeShown: false, OrderNo: not null })
        {
            await productRepository.BulkUpdateOrderAsync(kitToUpdate.OrderNo.Value);
            kitToUpdate.OrderNo = null;
        }
        
        var updatedKit = await kitRepository.UpdateAsync(kitToUpdate);
        return Result<KitVm>.Success(mapper.Map<KitVm>(updatedKit));
    }

    public async Task<Result<KitDetailsVm>> GetDetailsVmAsync(int id)
    {
        var kit = await kitRepository.GetFullKitWithMembershipsByIdAsync(id);
        if (kit is null)
        {
            return Result<KitDetailsVm>.Failure(Errors.Kits.DoesNotExit);
        }

        var kitDetailsVm = mapper.Map<KitDetailsVm>(kit);
        return Result<KitDetailsVm>.Success(kitDetailsVm);
    }

    public async Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm)
    {
        var matchingKits = kitRepository.GetFilteredKits(listKitsVm.NameFilter, listKitsVm.ItemId);
        matchingKits = matchingKits.OrderBy(kit => kit.Id);

        var pagination = await matchingKits.Paginate(listKitsVm.Pagination, mapper.ConfigurationProvider);

        var listItemVm = listKitsVm with { Pagination = pagination };
        return listItemVm;
    }

    public async Task<Result<KitVm>> GetKitVmForEditAsync(int id)
    {
        var kit = await kitRepository.GetFullKitWithMembershipsByIdAsync(id);
        if (kit is null)
        {
            return Result<KitVm>.Failure(Errors.Kits.DoesNotExit);
        }

        var kitDetailsVm = mapper.Map<KitVm>(kit);
        return Result<KitVm>.Success(kitDetailsVm);
    }

    public Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(int kitId)
    {
        var paginationVm = PaginationFactory.Default<ItemForListVm>();

        ListItemsForComponentsVm listItemsForComponentsVm = new()
        {
            Pagination = paginationVm,
            KitId = kitId
        };
        return GetFilteredListForComponentsAsync(listItemsForComponentsVm);
    }

    public async Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(
        ListItemsForComponentsVm itemsForComponentsVm)
    {
        if (!await kitRepository.ExistsAsync(itemsForComponentsVm.KitId))
        {
            return Result<ListItemsForComponentsVm>.Failure(Errors.Kits.DoesNotExit);
        }

        itemsForComponentsVm.KitComponentsIds =
            await (await kitRepository.GetComponentsIdsForSet(itemsForComponentsVm.KitId)).ToListAsync();
        var kitDetailsResult = await GetDetailsVmAsync(itemsForComponentsVm.KitId);
        if (kitDetailsResult.IsFailure)
        {
            return Result<ListItemsForComponentsVm>.Failure(kitDetailsResult.Error);
        }

        itemsForComponentsVm.KitDetailsVm = kitDetailsResult.Value;

        var allItems =
            itemRepository.GetFilteredItems(itemsForComponentsVm.NameFilter, itemsForComponentsVm.CategoryId)
                .OrderBy(item => item.Id);

        itemsForComponentsVm.Pagination =
            await allItems.Paginate(itemsForComponentsVm.Pagination, mapper.ConfigurationProvider);
        return Result<ListItemsForComponentsVm>.Success(itemsForComponentsVm);
    }

    public async Task<Result<ElementVm>> GetVmForAddingElementAsync(int kitId, int itemId)
    {
        var kit = await kitRepository.GetFullKitWithMembershipsByIdAsync(kitId);
        if (kit is null)
        {
            return Result<ElementVm>.Failure(Errors.Kits.DoesNotExit);
        }

        var item = await itemRepository.GetByIdAsync(itemId);
        if (item is null)
        {
            return Result<ElementVm>.Failure(Errors.Items.DoesNotExit);
        }
        
        var element = await kitRepository.GetElementAsync(kitId, itemId);
        if (element is not null)
        {
            return Result<ElementVm>.Failure(Errors.Elements.AlreadyExists);
        }
        
        ElementVm elementVm = new()
        {
            KitDetailsVm = mapper.Map<KitDetailsVm>(kit),
            Item = mapper.Map<ItemVm>(item),
            ItemsAmount = 1,
            PricePerItem = item.Price
        };
        return Result<ElementVm>.Success(elementVm);
    }

    public async Task<Result> AddElementAsync(ElementVm elementToCreateVm)
    {
        var elementToCreate = mapper.Map<Element>(elementToCreateVm);
        
        // Check if element already exists.
        if(await kitRepository.GetElementAsync(elementToCreate.KitId, elementToCreate.ItemId) is not null)
        {
            return Result.Failure(Errors.Elements.AlreadyExists);
        }
        
        // Check if kit and item exist.
        if(!await kitRepository.ExistsAsync(elementToCreate.KitId))
        {
            return Result.Failure(Errors.Kits.DoesNotExit);
        }
        
        if(!await itemRepository.ExistsAsync(elementToCreate.ItemId))
        {
            return Result.Failure(Errors.Items.DoesNotExit);
        }
        
        await kitRepository.AddComponentAsync(elementToCreate);
        return Result.Success();
    }
    
    public async Task<Result<ElementVm>> GetVmForEditingElementAsync(int kitId, int itemId)
    {
        var element = await kitRepository.GetElementAsync(kitId, itemId);
        if (element is null)
        {
            return Result<ElementVm>.Failure(Errors.Elements.DoesNotExist);
        }
        
        var elementToEdit = mapper.Map<ElementVm>(element);
        return Result<ElementVm>.Success(elementToEdit);
    }

    public async Task<Result<int>> GetElementCount(int id)
    {
            var kit = await kitRepository.GetFullKitWithMembershipsByIdAsync(id);
            if (kit is null)
            {
                return Result<int>.Failure(Errors.Kits.DoesNotExit);
            }

            var elementCount = kit.Elements.Count;
            return Result<int>.Success(elementCount);
    }

    public async Task<Result> EditElementAsync(ElementVm elementToEditVm)
    {
        var elementToEdit = await kitRepository.GetElementAsync(elementToEditVm.Id);
        if (elementToEdit is null)
        {
            return Result.Failure(Errors.Elements.DoesNotExist);
        }
        
        elementToEdit.ItemsAmount = elementToEditVm.ItemsAmount;
        elementToEdit.PricePerItem = elementToEditVm.PricePerItem;
        
        await kitRepository.UpdateElementAsync(elementToEdit);
        return Result.Success();
    }

    public async Task<Result> DeleteElementAsync(int kitId, int itemId)
    {
        var elementToDelete = await kitRepository.GetElementAsync(kitId, itemId);
        if (elementToDelete is null)
        {
            return Result.Failure(Errors.Elements.DoesNotExist);
        }
        
        await kitRepository.DeleteElementAsync(elementToDelete.Id);
        return Result.Success();
    }

    public async Task<Result> DeleteKitAsync(int id)
    {
        var kitToDelete = await kitRepository.GetByIdAsync(id);
        if (kitToDelete is null)
        {
            return Result.Failure(Errors.Kits.DoesNotExit);
        }

        try
        {
            await kitRepository.DeleteAsync(id);
            
            fileService.DeleteImage(kitToDelete.Image.FileUrl);
            if (kitToDelete.ShouldBeShown)
            {
                await productRepository.BulkUpdateOrderAsync(kitToDelete.OrderNo.Value);
            }
        }
        catch (ResourceNotFoundException)
        {
            return Result.Failure(Errors.Kits.DoesNotExit);
        }

        if (kitToDelete.Image is not null)
        {
            fileService.DeleteImage(kitToDelete.Image!.FileUrl);
        }

        return Result.Success();
    }

    private static bool ShouldSwitchImages(ImageVm? image)
    {
        return image is not null && image.FormFile is not null;
    }
}