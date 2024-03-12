using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Kit;
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
        kitToCreate.Price = 0;
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
            await _purchasableRepository.BulkUpdateOrderAsync(kit.OrderNo.Value);
        }

        if (kit.ShouldBeShown && kit.OrderNo is null)
            kit.OrderNo = await _purchasableRepository.GetNextOrderNumberAsync();
        var updatedKit = await _kitRepository.UpdateAsync(kit);
        return updatedKit.Id;
    }

    public async Task<KitDetailsVm> GetDetailsVmAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id) ??
                  throw new InvalidDataException("Kit does not exist!");
        var kitDetailsVm = _mapper.Map<KitDetailsVm>(kit);
        return kitDetailsVm;
    }

    public async Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm)
    {
        var matchingSets = _kitRepository.GetFilteredKits(listKitsVm.NameFilter, listKitsVm.ItemId);
        var pagination = await matchingSets.Paginate(listKitsVm.Pagination, _mapper.ConfigurationProvider);

        ListKitsVm listItemVm = new()
        {
            Pagination = pagination,
            ItemId = listKitsVm.ItemId,
            NameFilter = listKitsVm.NameFilter
        };
        return listItemVm;
    }

    public async Task<KitVm> GetKitVmForEditAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id) ??
                  throw new InvalidDataException("Kit does not exist!");
        var kitDetailsVm = _mapper.Map<KitVm>(kit);
        return kitDetailsVm;
    }

    public Task<ListItemsForComponentsVm> GetFilteredListForComponentsAsync(int kitId)
    {
        var paginationVm = PaginationFactory.Default<ItemForListVm>();

        ListItemsForComponentsVm listItemsForComponentsVm = new()
        {
            Pagination = paginationVm,
            KitId = kitId
        };
        return GetFilteredListForComponentsAsync(listItemsForComponentsVm);
    }

    public async Task<ListItemsForComponentsVm> GetFilteredListForComponentsAsync(
        ListItemsForComponentsVm itemsForComponentsVm)
    {
        if (!await _kitRepository.ExistsAsync(itemsForComponentsVm.KitId))
            throw new EntityNotFoundException(entityName: "Set");
        itemsForComponentsVm.KitComponentsIds =
            await (await _kitRepository.GetComponenetsIdsForSet(itemsForComponentsVm.KitId)).ToListAsync();
        itemsForComponentsVm.KitDetailsVm = await GetDetailsVmAsync(itemsForComponentsVm.KitId);

        var allItems =
            _itemRepository.GetFilteredItems(itemsForComponentsVm.NameFilter, itemsForComponentsVm.CategoryId);
        itemsForComponentsVm.Pagination =
            await allItems.Paginate(itemsForComponentsVm.Pagination, _mapper.ConfigurationProvider);
        return itemsForComponentsVm;
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
            ItemsAmmount = 1,
            PricePerItem = item.Price
        };
        return elementVm;
    }

    public async Task AddElementAsync(ElementVm elementToCreate)
    {
        var componentToCreate = _mapper.Map<Element>(elementToCreate);
        await _kitRepository.AddComponentAsync(componentToCreate);
        await _kitRepository.UpdateKitPriceAsync(componentToCreate.KitId);
    }

    public Task EditElementAsync(ElementVm elementToEdit)
    {
        var componentToEdit = _mapper.Map<Element>(elementToEdit);
        return _kitRepository.UpdateElementAsync(componentToEdit);
    }

    public async Task<ElementVm> GetVmForEdittingElementAsync(int kitId, int itemId)
    {
        var element = await _kitRepository.GetElementAsync(kitId, itemId) ??
                      throw new EntityNotFoundException(entityName: nameof(Element));
        var elementToEdit = _mapper.Map<ElementVm>(element);
        return elementToEdit;
    }

    public Task DeleteElementAsync(int kitId, int itemId)
    {
        return _kitRepository.DeleteElementAsync(kitId, itemId);
    }

    public async Task<DeleteKitResult> DeleteKitAsync(int id)
    {
        var kitToDelete = await _kitRepository.GetByIdAsync(id);
        if (kitToDelete is null) return DeleteKitResultsFactory.NotExist();

        try
        {
            await _kitRepository.DeleteAsync(id);
            _fileService.DeleteImage(kitToDelete.Image.FileUrl);
            if (kitToDelete.ShouldBeShown) await _purchasableRepository.BulkUpdateOrderAsync(kitToDelete.OrderNo.Value);
        }
        catch (ResourceNotFoundException)
        {
            return DeleteKitResultsFactory.NotExist();
        }
        catch (Exception ex)
        {
            return DeleteKitResultsFactory.Exception(ex);
        }

        if (kitToDelete.Image is not null) _fileService.DeleteImage(kitToDelete.Image!.FileUrl);
        return DeleteKitResultsFactory.Success();
    }

    private static bool ShouldSwitchImages(ImageVm? image)
    {
        return image is not null && image.FormFile is not null;
    }
}