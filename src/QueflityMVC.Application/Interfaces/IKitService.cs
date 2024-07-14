using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Interfaces;

public interface IKitService
{
    Task AddElementAsync(ElementVm elementVm);

    Task<int> CreateKitAsync(KitVm kitVm);

    Task DeleteElementAsync(int kitId, int itemId);

    Task<Result> DeleteKitAsync(int id);

    Task EditElementAsync(ElementVm elementVm);

    Task<int> EditKitAsync(KitVm editKitVm);

    Task<Result<KitDetailsVm>> GetDetailsVmAsync(int id);

    Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm);

    Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(int kitId);

    Task<Result<ListItemsForComponentsVm>> GetFilteredListForComponentsAsync(
        ListItemsForComponentsVm itemsForComponentsVm);

    Task<Result<KitVm>> GetKitVmForEditAsync(int id);

    Task<ElementVm> GetVmForAddingElementAsync(int kitId, int itemId);

    Task<ElementVm> GetVmForEditingElementAsync(int kitId, int itemId);

    Task<int> GetElementCount(int id);
}