using QueflityMVC.Application.Results.Kit;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.Interfaces;

public interface IKitService
{
    Task AddElementAsync(ElementVM elementVM);

    Task<int> CreateKitAsync(KitVM KitVM);

    Task DeleteElementAsync(int kitId, int itemId);

    Task<DeleteKitResult> DeleteKitAsync(int id);

    Task EditElementAsync(ElementVM elementVM);

    Task<int> EditKitAsync(KitVM editKitVM);

    Task<KitDetailsVM> GetDetailsVMAsync(int id);

    Task<ListKitsVM> GetFilteredListAsync(ListKitsVM listKitsVM);

    Task<ListItemsForComponentsVM> GetFilteredListForComponentsAsync(int id);

    Task<ListItemsForComponentsVM> GetFilteredListForComponentsAsync(ListItemsForComponentsVM listItemsForComponentsVM);

    Task<KitVM> GetKitVMForEditAsync(int id);

    Task<ElementVM> GetVMForAddingElementAsync(int kitId, int itemId);

    Task<ElementVM> GetVMForEdittingElementAsync(int kitId, int itemId);
}