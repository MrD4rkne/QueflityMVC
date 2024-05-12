using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Purchasable;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Infrastructure.Abstraction.Purchasables;

namespace QueflityMVC.Application.Services;

public class PurchasableEntityService : IPurchasableEntityService
{
    private readonly IEmailDispatcher _emailDispatcher;
    private readonly IMapper _mapper;
    private readonly IPurchasableRepository _purchasableRepository;
    private readonly IUserRepository _userRepository;

    public PurchasableEntityService(IMapper mapper, IPurchasableRepository purchasableRepository,
        IUserRepository userRepository, IEmailDispatcher emailDispatcher)
    {
        _mapper = mapper;
        _purchasableRepository = purchasableRepository;
        _userRepository = userRepository;
        _emailDispatcher = emailDispatcher;
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

    public async Task<Result> UpdateOrderAsync(EditOrderVm editOrderVm)
    {
        if (!IsOrderValid(editOrderVm.PurchasablesVMs))
            return Result.Failure(Errors.Purchasable.InvalidOrder);
        var purchasableModels =
            editOrderVm.PurchasablesVMs.Select(p => _mapper.Map<BasePurchasableEntity>(p)).ToList();
        if (!await _purchasableRepository.AreTheseAllVisiblePurchasablesAsync(purchasableModels))
            return Result.Failure(Errors.Purchasable.PurchasableMissingInOrder);
        await _purchasableRepository.UpdatePurchasablesOrderAsync(purchasableModels);
        return Result.Success();
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

    private static bool IsOrderValid(List<PurchasableVm> purchasables)
    {
        if (!purchasables.All(p => p.OrderNo >= 0)) return false;
        var orders = purchasables.Select(purchasable => purchasable.OrderNo).ToList();
        return IsOrderFull(orders);
    }

    private static bool IsOrderFull(List<uint?> ids)
    {
        ids.Sort();
        return !ids.Where((t, i) => t != i).Any();
    }
}