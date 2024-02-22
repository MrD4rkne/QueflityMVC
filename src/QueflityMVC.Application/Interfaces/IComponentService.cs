using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.Interfaces;

public interface IComponentService
{
    Task<ListComponentsVM> GetFilteredListAsync(ListComponentsVM listComponentsVM);

    Task<int> CreateComponentAsync(ComponentVM componentToCreateVM);

    Task<ComponentVM?> GetComponentVMForEditAsync(int id);

    Task UpdateComponentAsync(ComponentVM componentToEditVM);

    Task DeleteComponentAsync(int id);
}