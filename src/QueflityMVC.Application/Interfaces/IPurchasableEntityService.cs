using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;

public interface IPurchasableEntityService
{
    Task<EditOrderVm> GetEntitiesOrderVm();

    Task<Result> UpdateOrderAsync(EditOrderVm editOrderVm);

    Task<DashboardVm> GetDashboardVmAsync();
}