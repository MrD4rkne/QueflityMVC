using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

    public IQueryable<BasePurchasableEntity> GetVisibileEntities()
    {
        return _dbContext.Set<BasePurchasableEntity>()
            .Where(x => x.ShouldBeShown)
            .Include(x => x.Image);
    }

    public async Task UpdatePurchasablesOrderAsync(List<BasePurchasableEntity> purchasableModels) { 
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
}
