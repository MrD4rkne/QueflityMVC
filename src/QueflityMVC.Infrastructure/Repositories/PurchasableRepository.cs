using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Infrastructure.Repositories;

public class PurchasableRepository : IPurchasableRepository
{
    private readonly Context _dbContext;

    public PurchasableRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> AreTheseAllVisiblePurchasablesAsync(List<BasePurchasableEntity> purchasableModels)
    {
        bool isAnyNotInList = await _dbContext.Set<BasePurchasableEntity>()
            .Where(x => !purchasableModels.Contains(x))
            .AnyAsync(x => x.ShouldBeShown);
        return !isAnyNotInList;
    }

    public async Task<uint> GetNextOrderNumberAsync()
    {
        uint? lastOrderNo = await _dbContext.Set<BasePurchasableEntity>().MaxAsync(x => x.OrderNo);
        if (lastOrderNo is null)
        {
            return 0;
        }
        return lastOrderNo.Value + 1;
    }

    public IQueryable<BasePurchasableEntity> GetVisibileEntities()
    {
        return _dbContext.Set<BasePurchasableEntity>()
            .AsNoTracking()
            .Where(x => x.ShouldBeShown)
            .Include(x => x.Image);
    }

    public async Task UpdateOrderNoAsync(BasePurchasableEntity basePurchasableEntity)
    {
        uint orderNo = await GetNextOrderNumberAsync();
        basePurchasableEntity.OrderNo = orderNo;
        _dbContext.Update(basePurchasableEntity);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdatePurchasablesOrderAsync(List<BasePurchasableEntity> purchasableModels)
    {
        try
        {
            var entities = _dbContext.Set<BasePurchasableEntity>().Where(x => purchasableModels.Contains(x));
            await entities.ForEachAsync(x => x.OrderNo = purchasableModels.First(p => p.Id == x.Id).OrderNo);
            await _dbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task BulkUpdateOrderAsync(uint pivot)
    {
        await _dbContext.Set<BasePurchasableEntity>()
            .Where(x => x.OrderNo >= pivot)
            .ForEachAsync(x => x.OrderNo--);
    }
}