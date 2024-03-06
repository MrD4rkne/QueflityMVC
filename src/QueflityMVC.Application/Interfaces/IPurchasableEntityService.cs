using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;

public interface IPurchasableEntityService
{
    Task<EditOrderVM> GetEnitiesOrderVM();

    Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVM editOrderVM);
}