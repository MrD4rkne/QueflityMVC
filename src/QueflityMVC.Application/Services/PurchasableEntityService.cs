using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.Results;
using QueflityMVC.Application.ViewModels.Product;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Services;

public class ProductEntityService(
    IMapper mapper,
    IProductRepository productRepository,
    IUserRepository userRepository)
    : IProductEntityService
{
    public async Task<EditOrderVm> GetEntitiesOrderVm()
    {
        var models = await productRepository.GetVisibleEntities()
            .OrderBy(x => x.OrderNo)
            .ToListAsync();
        var results = models.Select(x => mapper.Map<ProductVm>(x))
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
        {
            return Result.Failure(Errors.Product.InvalidOrder);
        }

        var productModels =
            editOrderVm.ProductsVMs.Select(p => mapper.Map<Product>(p)).ToList();
        if (!await productRepository.AreTheseAllVisibleProductsAsync(productModels))
        {
            return Result.Failure(Errors.Product.ProductMissingInOrder);
        }

        await productRepository.UpdateProductsOrderAsync(productModels);
        return Result.Success();
    }

    public async Task<DashboardVm> GetDashboardVmAsync()
    {
        var products = productRepository.GetVisibleProductsForDashboard();
        DashboardVm dashboard = new()
        {
            Products = await products.Select(x => mapper.Map<ProductForCardVm>(x)).ToListAsync()
        };
        return dashboard;
    }

    private static bool IsOrderValid(List<ProductVm> products)
    {
        if (!products.All(p => p.OrderNo >= 0))
        {
            return false;
        }

        var orders = products.Select(product => product.OrderNo).ToList();
        return IsOrderFull(orders);
    }

    private static bool IsOrderFull(List<uint?> ids)
    {
        ids.Sort();
        return !ids.Where((t, i) => t != i).Any();
    }
}