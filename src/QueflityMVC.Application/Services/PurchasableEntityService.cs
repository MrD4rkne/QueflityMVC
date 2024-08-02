using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Abstraction.Interfaces;

namespace QueflityMVC.Application.Services;

public class ProductEntityService : IProductEntityService
{
    private readonly IEmailDispatcher _emailDispatcher;
    private readonly IMapper _mapper;
    private readonly IProductRepository _purchasableRepository;
    private readonly IUserRepository _userRepository;

    public ProductEntityService(IMapper mapper, IProductRepository purchasableRepository,
        IUserRepository userRepository, IEmailDispatcher emailDispatcher)
    {
        _mapper = mapper;
        _purchasableRepository = purchasableRepository;
        _userRepository = userRepository;
        _emailDispatcher = emailDispatcher;
    }

    public async Task<EditOrderVm> GetEntitiesOrderVm()
    {
        var models = await _purchasableRepository.GetVisibleEntities()
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        var results = models.Select(x => _mapper.Map<ProductVm>(x))
            .ToList();
        var editVm = new EditOrderVm
        {
            ProductsVMs = results
        };
        return editVm;
    }

    public async Task<Result> UpdateOrderAsync(EditOrderVm editOrderVm)
    {
        if (!IsOrderValid(editOrderVm.ProductsVMs))
            return Result.Failure(Errors.Product.InvalidOrder);
        var purchasableModels =
            editOrderVm.ProductsVMs.Select(p => _mapper.Map<Product>(p)).ToList();
        if (!await _purchasableRepository.AreTheseAllVisibleProductsAsync(purchasableModels))
            return Result.Failure(Errors.Product.ProductMissingInOrder);
        await _purchasableRepository.UpdateProductsOrderAsync(purchasableModels);
        return Result.Success();
    }

    public async Task<DashboardVm> GetDashboardVmAsync()
    {
        var purchasables = _purchasableRepository.GetVisibleProductsForDashboard();
        DashboardVm dashboard = new()
        {
            Products = await purchasables.Select(x => _mapper.Map<ProductForDashboardVm>(x)).ToListAsync()
        };
        return dashboard;
    }

    private static bool IsOrderValid(List<ProductVm> purchasables)
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