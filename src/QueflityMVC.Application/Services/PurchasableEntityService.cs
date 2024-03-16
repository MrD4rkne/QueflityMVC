using AutoMapper;
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

    public async Task<EditOrderVm> GetEntitiesOrderVm()
    {
        var models = await _purchasableRepository.GetVisibileEntities().OrderBy(x => x.OrderNo).ToListAsync();
        var results = models.Select(x => _mapper.Map<PurchasableVm>(x)).ToList();
        var editVm = new EditOrderVm
        {
            PurchasablesVMs = results
        };
        return editVm;
    }

    public async Task<UpdateOrderResult> UpdateOrderAsync(EditOrderVm editOrderVm)
    {
        try
        {
            if (!IsProperOrder(editOrderVm.PurchasablesVMs))
                return UpdateOrderResultsFactory.NotValidOrder();
            var purchasableModels =
                editOrderVm.PurchasablesVMs.Select(p => _mapper.Map<BasePurchasableEntity>(p)).ToList();
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

    public async Task<DashboardVm> GetDashboardVmAsync()
    {
        var purchasables = _purchasableRepository.GetVisiblePurchasablesForDashboard();
        DashboardVm dashboard = new()
        {
            Purchasables = await purchasables.Select(x => _mapper.Map<PurchasableForDashboardVm>(x)).ToListAsync()
        };
        return dashboard;
    }

    private bool IsProperOrder(List<PurchasableVm> purchasables)
    {
        if (!purchasables.All(p => p.OrderNo >= 0)) return false;
        var orders = purchasables.Select(purchasables => purchasables.OrderNo).ToList();
        orders.Sort();
        for (var i = 0; i < orders.Count; i++)
            if (orders[i] != i)
                return false;
        return true;
    }
}