using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Item;
using QueflityMVC.Application.Results.Kit;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.Pagination;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class KitService : IKitService
{
    private readonly IKitRepository _kitRepository;
    private readonly IItemRepository _itemRepository;
    private readonly IMapper _mapper;
    private readonly IFileService _fileService;

    public KitService(IKitRepository kitRepository, IItemRepository itemRepository, IMapper mapper, IFileService fileService)
    {
        _kitRepository = kitRepository;
        _itemRepository = itemRepository;
        _mapper = mapper;
        _fileService = fileService;
    }

    public async Task<int> CreateKitAsync(KitVM kitVM)
    {
        kitVM.Image!.FileUrl = await _fileService.UploadFileAsync(kitVM.Image.FormFile);
        var kitToCreate = _mapper.Map<Kit>(kitVM);
        kitToCreate.Price = 0;

        return await _kitRepository.AddAsync(kitToCreate);
    }

    public async Task<int> EditKitAsync(KitVM editKitVM)
    {
        if (ShouldSwitchImages(editKitVM?.Image))
        {
            _fileService.DeleteImage(editKitVM.Image.FileUrl);
            editKitVM.Image.FileUrl = await _fileService.UploadFileAsync(editKitVM.Image.FormFile!);
        }

        var item = _mapper.Map<Kit>(editKitVM);
        var updatedItem = await _kitRepository.UpdateAsync(item) ?? throw new ArgumentException("Item set does not exist!");
        return updatedItem.Id;
    }

    private static bool ShouldSwitchImages(ImageVM? image)
    {
        return image is not null && image.FormFile is not null;
    }

    public async Task<KitDetailsVM> GetDetailsVMAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id) ?? throw new InvalidDataException("Kit does not exist!");
        var kitDetailsVM = _mapper.Map<KitDetailsVM>(kit);
        return kitDetailsVM;
    }

    public async Task<ListKitsVM> GetFilteredListAsync(ListKitsVM listKitsVM)
    {
        var matchingSets = _kitRepository.GetFilteredKits(listKitsVM.NameFilter, listKitsVM.ItemId);
        var pagination = await matchingSets.Paginate(listKitsVM.Pagination, _mapper.ConfigurationProvider);

        ListKitsVM listItemVM = new()
        {
            Pagination = pagination,
            ItemId = listKitsVM.ItemId,
            NameFilter = listKitsVM.NameFilter
        };
        return listItemVM;
    }

    public async Task<KitVM> GetKitVMForEditAsync(int id)
    {
        var kit = await _kitRepository.GetFullKitWithMembershipsByIdAsync(id) ?? throw new InvalidDataException("Kit does not exist!");
        var kitDetailsVM = _mapper.Map<KitVM>(kit);
        return kitDetailsVM;
    }

    public Task<ListItemsForComponentsVM> GetFilteredListForComponentsAsync(int kitId)
    {
        PaginationVM<ItemForListVM> paginationVM = PaginationFactory.Default<ItemForListVM>();

        ListItemsForComponentsVM listItemsForComponentsVM = new()
        {
            Pagination = paginationVM,
            kitId = kitId
        };
        return GetFilteredListForComponentsAsync(listItemsForComponentsVM);
    }

    public async Task<ListItemsForComponentsVM> GetFilteredListForComponentsAsync(ListItemsForComponentsVM itemsForComponentsVM)
    {
        if (!await _kitRepository.ExistsAsync(itemsForComponentsVM.kitId))
        {
            throw new EntityNotFoundException(entityName: "Set");
        }
        itemsForComponentsVM.KitComponentsIds = await (await _kitRepository.GetComponenetsIdsForSet(itemsForComponentsVM.kitId)).ToListAsync();
        itemsForComponentsVM.KitDetailsVM = await GetDetailsVMAsync(itemsForComponentsVM.kitId);

        IQueryable<Item> allItems = _itemRepository.GetFilteredItems(itemsForComponentsVM.NameFilter, itemsForComponentsVM.CategoryId);
        itemsForComponentsVM.Pagination = await allItems.Paginate<Item, ItemForListVM>(itemsForComponentsVM.Pagination, _mapper.ConfigurationProvider);
        return itemsForComponentsVM;
    }

    public async Task<ElementVM> GetVMForAddingElementAsync(int kitId, int itemId)
    {
        Kit? kit = await _kitRepository.GetByIdAsync(kitId) ?? throw new EntityNotFoundException(entityName: nameof(Kit));
        Item? item = await _itemRepository.GetByIdAsync(itemId) ?? throw new EntityNotFoundException(entityName: nameof(Item));
        ElementVM elementVM = new()
        {
            KitDetailsVM = _mapper.Map<KitDetailsVM>(kit),
            Item = _mapper.Map<ItemVM>(item),
            ItemsAmmount = 1,
            PricePerItem = item.Price
        };

        return elementVM;
    }

    public async Task AddElementAsync(ElementVM elementToCreate)
    {
        var componentToCreate = _mapper.Map<Element>(elementToCreate);
        await _kitRepository.AddComponentAsync(componentToCreate);
        await _kitRepository.UpdateKitPriceAsync(componentToCreate.KitId);
    }

    public Task EditElementAsync(ElementVM elementToEdit)
    {
        var componentToEdit = _mapper.Map<Element>(elementToEdit);
        return _kitRepository.UpdateElementAsync(componentToEdit);
    }

    public async Task<ElementVM> GetVMForEdittingElementAsync(int kitId, int itemId)
    {
        Element? element = await _kitRepository.GetElementAsync(kitId, itemId);
        if (element is null)
        {
            throw new EntityNotFoundException(entityName: nameof(Element));
        }

        ElementVM elementToEdit = _mapper.Map<ElementVM>(element);
        return elementToEdit;
    }

    public Task DeleteElementAsync(int kitId, int itemId)
    {
        return _kitRepository.DeleteElementAsync(kitId, itemId);
    }

    public async Task<DeleteKitResult> DeleteKitAsync(int id) {
        Kit? kitToDelete = await _kitRepository.GetByIdAsync(id);
        if (kitToDelete is null)
        {
            return DeleteKitResultsFactory.NotExist();
        }

        try
        {
            await _kitRepository.DeleteAsync(id);
        }
        catch (ResourceNotFoundException)
        {
            return DeleteKitResultsFactory.NotExist();
        }
        catch (Exception ex)
        {
            return DeleteKitResultsFactory.Exception(ex);
        }

        if (kitToDelete.Image is not null)
        {
            _fileService.DeleteImage(kitToDelete.Image!.FileUrl);
        }
        return DeleteKitResultsFactory.Success();
    }
}