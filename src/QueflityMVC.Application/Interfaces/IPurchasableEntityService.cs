using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;

public interface IPurchasableEntityService
{
    Task<EditOrderVm> GetEntitiesOrderVm();

    Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVm editOrderVm);

    Task<DashboardVm> GetDashboardVmAsync();
    
    Task<MessageVm> GetContactVmAsync(int id);
}