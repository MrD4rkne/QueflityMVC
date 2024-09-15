using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Interfaces;

public interface IKitService
{
    Task<Result> AddElementAsync(ElementVm elementVm);

    Task<Result<KitVm>> CreateKitAsync(KitVm kitVm);

    Task<Result> DeleteElementAsync(int kitId, int itemId);

    Task<Result> DeleteKitAsync(int id);

    Task<Result> EditElementAsync(ElementVm elementVm);

    Task<Result<KitVm>> EditKitAsync(KitVm editKitVm);

    Task<Result<KitDetailsVm>> GetDetailsVmAsync(int id);

    Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm);

    Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(int kitId);

    Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(
        ListItemsForComponentsVm itemsForComponentsVm);

    Task<Result<KitVm>> GetKitVmForEditAsync(int id);

    Task<Result<ElementVm>> GetVmForAddingElementAsync(int kitId, int itemId);

    Task<Result<ElementVm>> GetVmForEditingElementAsync(int kitId, int itemId);

    Task<Result<int>> GetElementCount(int id);
}