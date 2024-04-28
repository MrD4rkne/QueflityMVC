using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Other;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;

public interface IPurchasableEntityService
{
    Task<EditOrderVm> GetEntitiesOrderVm();

    Task<Result> UpdateOrderAsync(EditOrderVm editOrderVm);

    Task<DashboardVm> GetDashboardVmAsync();

    Task<Result<MessageVm>> GetContactVmAsync(int id, string email);
}