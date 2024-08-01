using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Persistence.Repositories;

public class ProductRepository(Context dbContext) : IProductRepository
{
    public async Task<bool> AreTheseAllVisibleProductsAsync(List<Product> purchasableModels)
    {
        var isAnyNotInList = await dbContext.Set<Product>()
            .Where(x => !purchasableModels.Contains(x))
            .AnyAsync(x => x.ShouldBeShown);
        return !isAnyNotInList;
    }

    public async Task<uint> GetNextOrderNumberAsync()
    {
        var lastOrderNo = await dbContext.Set<Product>().MaxAsync(x => x.OrderNo);
        if (lastOrderNo is null) return 0;
        return lastOrderNo.Value + 1;
    }

    public IQueryable<Product> GetVisibleEntities()
    {
        return dbContext.Set<Product>()
            .AsNoTracking()
            .Where(x => x.ShouldBeShown)
            .OrderBy(x => x.OrderNo)
            .Include(x => x.Image)
            .Include(x => (x as Kit).Elements);
    }

    public async Task UpdateOrderNoAsync(Product purchasable)
    {
        var orderNo = await GetNextOrderNumberAsync();
        purchasable.OrderNo = orderNo;
        dbContext.Update(purchasable);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductsOrderAsync(List<Product> purchasableModels)
    {
        var entities = dbContext.Set<Product>().Where(x => purchasableModels.Contains(x));
        await entities.ForEachAsync(x => x.OrderNo = purchasableModels.First(p => p.Id == x.Id).OrderNo);
        await dbContext.SaveChangesAsync();
    }

    public IQueryable<Product> GetVisibleProductsForDashboard()
    {
        return dbContext.Set<Product>()
            .AsNoTracking()
            .Where(x => x.ShouldBeShown)
            .OrderBy(x => x.OrderNo)
            .Include(x => x.Image)
            .Include(x => (x as Item).Category)
            .Include(x => (x as Item).Components)
            .Include(x => (x as Kit).Elements)
            .ThenInclude(cp => cp.Item)
            .ThenInclude(el => el.Image);
    }

    public Task<Product?> GetByIdAsync(int id)
    {
        return dbContext.Set<Product>()
            .AsNoTracking()
            .Include(x => x.Image)
            .Include(x => (x as Kit).Elements)
            .FirstOrDefaultAsync(purchasable => purchasable.Id == id);
    }
}