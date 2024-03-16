using QueflityMVC.Application.Results.Kit;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Interfaces;

public interface IKitService
{
    Task AddElementAsync(ElementVm elementVm);

    Task<int> CreateKitAsync(KitVm kitVm);

    Task DeleteElementAsync(int kitId, int itemId);

    Task<DeleteKitResult> DeleteKitAsync(int id);

    Task EditElementAsync(ElementVm elementVm);

    Task<int> EditKitAsync(KitVm editKitVm);

    Task<KitDetailsVm> GetDetailsVmAsync(int id);

    Task<ListKitsVm> GetFilteredListAsync(ListKitsVm listKitsVm);

    Task<ListItemsForComponentsVm> GetFilteredListForComponentsAsync(int id);

    Task<ListItemsForComponentsVm> GetFilteredListForComponentsAsync(ListItemsForComponentsVm listItemsForComponentsVm);

    Task<KitVm> GetKitVmForEditAsync(int id);

    Task<ElementVm> GetVmForAddingElementAsync(int kitId, int itemId);

    Task<ElementVm> GetVmForEditingElementAsync(int kitId, int itemId);
}