using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Product;

namespace QueflityMVC.Application.Interfaces;

public interface IProductEntityService
{
    Task<EditOrderVm> GetEntitiesOrderVm();

    Task<Result> UpdateOrderAsync(EditOrderVm editOrderVm);

    Task<DashboardVm> GetDashboardVmAsync();
}