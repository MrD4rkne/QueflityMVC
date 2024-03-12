using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.Interfaces;

public interface IComponentService
{
    Task<ListComponentsVm> GetFilteredListAsync(ListComponentsVm listComponentsVm);

    Task<int> CreateComponentAsync(ComponentVm componentToCreateVm);

    Task<ComponentVm?> GetComponentVmForEditAsync(int id);

    Task UpdateComponentAsync(ComponentVm componentToEditVm);

    Task DeleteComponentAsync(int id);
}