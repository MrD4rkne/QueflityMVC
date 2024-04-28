using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Persistence.Repositories;

public class PurchasableRepository(Context dbContext) : IPurchasableRepository
{
    public async Task<bool> AreTheseAllVisiblePurchasablesAsync(List<BasePurchasableEntity> purchasableModels)
    {
        var isAnyNotInList = await dbContext.Set<BasePurchasableEntity>()
            .Where(x => !purchasableModels.Contains(x))
            .AnyAsync(x => x.ShouldBeShown);
        return !isAnyNotInList;
    }

    public async Task<uint> GetNextOrderNumberAsync()
    {
        var lastOrderNo = await dbContext.Set<BasePurchasableEntity>().MaxAsync(x => x.OrderNo);
        if (lastOrderNo is null) return 0;
        return lastOrderNo.Value + 1;
    }

    public IQueryable<BasePurchasableEntity> GetVisibileEntities()
    {
        return dbContext.Set<BasePurchasableEntity>()
            .AsNoTracking()
            .Where(x => x.ShouldBeShown)
            .OrderBy(x => x.OrderNo)
            .Include(x => x.Image);
    }

    public async Task UpdateOrderNoAsync(BasePurchasableEntity basePurchasableEntity)
    {
        var orderNo = await GetNextOrderNumberAsync();
        basePurchasableEntity.OrderNo = orderNo;
        dbContext.Update(basePurchasableEntity);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdatePurchasablesOrderAsync(List<BasePurchasableEntity> purchasableModels)
    {
        var entities = dbContext.Set<BasePurchasableEntity>().Where(x => purchasableModels.Contains(x));
        await entities.ForEachAsync(x => x.OrderNo = purchasableModels.First(p => p.Id == x.Id).OrderNo);
        await dbContext.SaveChangesAsync();
    }

    public IQueryable<BasePurchasableEntity> GetVisiblePurchasablesForDashboard()
    {
        return dbContext.Set<BasePurchasableEntity>()
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

    public Task<BasePurchasableEntity?> GetByIdAsync(int id)
    {
        return dbContext.Set<BasePurchasableEntity>()
            .AsNoTracking()
            .Include(x => x.Image)
            .FirstOrDefaultAsync(purchasable => purchasable.Id == id);
    }

    public async Task BulkUpdateOrderAsync(uint pivot)
    {
        await dbContext.Set<BasePurchasableEntity>()
            .Where(x => x.OrderNo >= pivot)
            .ForEachAsync(x => x.OrderNo--);
    }
}