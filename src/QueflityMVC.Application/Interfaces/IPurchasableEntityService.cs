using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;

public interface IPurchasableEntityService
{
    Task<EditOrderVm> GetEnitiesOrderVm();

    Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVm editOrderVm);

    Task<DashboardVm> GetDashboardVmAsync();
}