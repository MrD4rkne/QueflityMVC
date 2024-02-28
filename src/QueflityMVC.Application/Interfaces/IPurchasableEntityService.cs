using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;
public interface IPurchasableEntityService
{
    Task<EditOrderVM> GetEnitiesOrderVM();
    Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVM editOrderVM);
}
