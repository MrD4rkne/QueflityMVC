using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results.Purchasable;
using QueflityMVC.Application.ViewModels.Purchasable;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.Services;
public class PurchasableEntityService : IPurchasableEntityService
{
    private readonly IMapper _mapper;
    private readonly IPurchasableRepository _purchasableRepository;

    public PurchasableEntityService(IMapper mapper, IPurchasableRepository purchasableRepository)
    {
        _mapper = mapper;
        _purchasableRepository = purchasableRepository;
    }


    public async Task<EditOrderVM> GetEnitiesOrderVM()
    {
        var purchasableTypes = Enum.GetValues<PurchasableType>();
        if (purchasableTypes.Length == 0)
        {
            throw new InvalidOperationException("No purchasable types found");
        }

        var models = await _purchasableRepository.GetVisibileEntities().OrderBy(x=> x.OrderNo).ToListAsync();
        var results = models.Select(x=> _mapper.Map<PurchasableVM>(x)).ToList();
        var editVM = new EditOrderVM
        {
            PurchasablesVMs = results
        };
        return editVM;
    }

    public async Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVM editOrderVM)
    {
        try
        {
            if (!IsProperOrder(editOrderVM.PurchasablesVMs))
                return UpdateOrderResultsFactory.NotValidOrder();
            var purchasableModels = editOrderVM.PurchasablesVMs.Select(p => _mapper.Map<Domain.Common.BasePurchasableEntity>(p)).ToList();
            if (!await _purchasableRepository.AreTheseAllVisiblePurchasablesAsync(purchasableModels))
                return UpdateOrderResultsFactory.MissingPurchasable();
            await _purchasableRepository.UpdatePurchasablesOrderAsync(purchasableModels);
            return UpdateOrderResultsFactory.Success();
        }
        catch (Exception ex)
        {
            return UpdateOrderResultsFactory.Exception(ex);
        }
    }

    private bool IsProperOrder(List<PurchasableVM> purchasables)
    {
        if(!purchasables.All(p => p.OrderNo >= 0))
        {
            return false;
        }
        var orders = purchasables.Select(purchasables => purchasables.OrderNo).ToList();
        orders.Sort();
        for(int i = 0; i < orders.Count; i++)
        {
            if (orders[i] != i)
            {
                return false;
            }
        }
        return true;
    }
}
