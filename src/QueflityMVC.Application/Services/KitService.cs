using AutoMapper;
using Microsoft.EntityFrameworkCore;
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

public class KitService : IKitService
{
    private readonly IFileService _fileService;
    private readonly IItemRepository _itemRepository;
    private readonly IKitRepository _kitRepository;
    private readonly IMapper _mapper;
    private readonly IPurchasableRepository _purchasableRepository;

    public KitService(IKitRepository kitRepository, IItemRepository itemRepository, IMapper mapper,
        IFileService fileService, IPurchasableRepository purchasableRepository)
    {
        _kitRepository = kitRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _fileService = fileService;
        _purchasableRepository = purchasableRepository;
    }

    public async Task<int> CreateKitAsync(KitVm kitVm)
    {
        kitVm.Image!.FileUrl = await _fileService.UploadFileAsync(kitVm.Image.FormFile);
        var kitToCreate = _mapper.Map<Kit>(kitVm);
        return await _kitRepository.AddAsync(kitToCreate);
    }

    public async Task<int> EditKitAsync(KitVm editKitVm)
    {
        if (ShouldSwitchImages(editKitVm?.Image))
        {
            _fileService.DeleteImage(editKitVm.Image.FileUrl);
            editKitVm.Image.FileUrl = await _fileService.UploadFileAsync(editKitVm.Image.FormFile!);
        }

        var kit = _mapper.Map<Kit>(editKitVm);
        if (!kit.ShouldBeShown)
        {
            kit.OrderNo = null;
        }

        if (kit.ShouldBeShown && kit.OrderNo is null)
            kit.OrderNo = await _purchasableRepository.GetNextOrderNumberAsync();
        var updatedKit = await _kitRepository.UpdateAsync(kit);
        return updatedKit.Id;
    }

    public async Task<Result<KitDetailsVm>> GetDetailsVmAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id);
        if (kit is null) return Result<KitDetailsVm>.Failure(Errors.Kits.DoesNotExit);

        var kitDetailsVm = _mapper.Map<KitDetailsVm>(kit);
        return Result<KitDetailsVm>.Success(kitDetailsVm);
    }

    public async Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm)
    {
        var matchingSets = _kitRepository.GetFilteredKits(listKitsVm.NameFilter, listKitsVm.ItemId);
        var pagination = await matchingSets.Paginate(listKitsVm.Pagination, _mapper.ConfigurationProvider);

        ListKitsVm listItemVm = listKitsVm with { Pagination = pagination };
        return listItemVm;
    }

    public async Task<Result<KitVm>> GetKitVmForEditAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id);
        if (kit is null) return Result<KitVm>.Failure(Errors.Kits.DoesNotExit);

        var kitDetailsVm = _mapper.Map<KitVm>(kit);
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
        if (!await _kitRepository.ExistsAsync(itemsForComponentsVm.KitId))
            return Result<ListItemsForComponentsVm>.Failure(Errors.Kits.DoesNotExit);

        itemsForComponentsVm.KitComponentsIds =
            await (await _kitRepository.GetComponentsIdsForSet(itemsForComponentsVm.KitId)).ToListAsync();
        var kitDetailsResult = await GetDetailsVmAsync(itemsForComponentsVm.KitId);
        if (kitDetailsResult.IsFailure) return Result<ListItemsForComponentsVm>.Failure(kitDetailsResult.Error);

        var allItems =
            _itemRepository.GetFilteredItems(itemsForComponentsVm.NameFilter, itemsForComponentsVm.CategoryId);
        itemsForComponentsVm.Pagination =
            await allItems.Paginate(itemsForComponentsVm.Pagination, _mapper.ConfigurationProvider);
        return Result<ListItemsForComponentsVm>.Success(itemsForComponentsVm);
    }

    public async Task<ElementVm> GetVmForAddingElementAsync(int kitId, int itemId)
    {
        var kit = await _kitRepository.GetByIdAsync(kitId) ??
                  throw new EntityNotFoundException(entityName: nameof(Kit));
        var item = await _itemRepository.GetByIdAsync(itemId) ??
                   throw new EntityNotFoundException(entityName: nameof(Item));
        ElementVm elementVm = new()
        {
            KitDetailsVm = _mapper.Map<KitDetailsVm>(kit),
            Item = _mapper.Map<ItemVm>(item),
            ItemsAmount = 1,
            PricePerItem = item.Price
        };
        return elementVm;
    }

    public async Task AddElementAsync(ElementVm elementToCreate)
    {
        var componentToCreate = _mapper.Map<Element>(elementToCreate);
        await _kitRepository.AddComponentAsync(componentToCreate);
    }

    public Task EditElementAsync(ElementVm elementToEdit)
    {
        var componentToEdit = _mapper.Map<Element>(elementToEdit);
        return _kitRepository.UpdateElementAsync(componentToEdit);
    }

    public async Task<ElementVm> GetVmForEditingElementAsync(int kitId, int itemId)
    {
        var element = await _kitRepository.GetElementAsync(kitId, itemId) ??
                      throw new EntityNotFoundException(entityName: nameof(Element));
        var elementToEdit = _mapper.Map<ElementVm>(element);
        return elementToEdit;
    }

    public Task<int> GetElementCount(int id)
    {
        return _kitRepository.GetElementCount(id);
    }

    public async Task DeleteElementAsync(int kitId, int itemId)
    {
        await _kitRepository.DeleteElementAsync(kitId, itemId);
    }

    public async Task<Result> DeleteKitAsync(int id)
    {
        var kitToDelete = await _kitRepository.GetByIdAsync(id);
        if (kitToDelete is null) return Result.Failure(Errors.Kits.DoesNotExit);

        try
        {
            await _kitRepository.DeleteAsync(id);
            _fileService.DeleteImage(kitToDelete.Image.FileUrl);
            if (kitToDelete.ShouldBeShown)
            {
                await _kitRepository.BulkUpdateOrderAsync(kitToDelete.OrderNo.Value);
            }
        }
        catch (ResourceNotFoundException)
        {
            return Result.Failure(Errors.Kits.DoesNotExit);
        }

        if (kitToDelete.Image is not null) _fileService.DeleteImage(kitToDelete.Image!.FileUrl);
        return Result.Success();
    }

    private static bool ShouldSwitchImages(ImageVm? image)
    {
        return image is not null && image.FormFile is not null;
    }
}