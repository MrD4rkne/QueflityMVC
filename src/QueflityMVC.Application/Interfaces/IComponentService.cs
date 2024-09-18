using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.Interfaces;

public interface IComponentService
{
    Task<ListComponentsVm> GetFilteredListAsync(ListComponentsVm listComponentsVm);

    Task<Result> CreateComponentAsync(ComponentVm componentToCreateVm);

    Task<Result<ComponentVm>> GetComponentVmForEditAsync(int id);

    Task<Result> UpdateComponentAsync(ComponentVm componentToEditVm);

    Task<Result> DeleteComponentAsync(int id);
}