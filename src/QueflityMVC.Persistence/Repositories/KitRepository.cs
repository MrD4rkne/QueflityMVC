using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Persistence.Repositories;

public class KitRepository(Context dbContext) : BasePurchasableRepository<Kit>(dbContext), IKitRepository
{
    public override Task<Kit?> GetByIdAsync(int entityId)
    {
        return _dbContext.Kits
            .AsNoTracking()
            .Include(kit => kit.Image)
            .FirstOrDefaultAsync(kit => kit.Id == entityId);
    }

    public async Task<IQueryable<int>> GetComponentsIdsForSet(int kitId)
    {
        var set = await GetFullKitWithMembershipsByIdAsync(kitId) ?? throw new ResourceNotFoundException();
        var componentsIds = GetKitComponents(kitId).Select(x => x.ItemId);
        return componentsIds;
    }

    public IQueryable<Element> GetKitComponents(int kitId)
    {
        return _dbContext.SetElements
            .AsNoTracking()
            .Where(x => x.KitId == kitId);
    }

    public IQueryable<Kit> GetFilteredKits(string? searchName = default, int? itemId = null)
    {
        var itemsSource = GetAll();
        if (itemId.HasValue) itemsSource = itemsSource.Where(x => x.Elements.Any(y => y.ItemId == itemId));
        if (!string.IsNullOrEmpty(searchName)) itemsSource = itemsSource.Where(x => x.Name.StartsWith(searchName));
        return itemsSource;
    }

    public Task<Kit?> GetFullKitWithMembershipsByIdAsync(int id)
    {
        return _dbContext.Set<Kit>()
            .Include(z => z.Image)
            .Include(x => x.Elements)
            .ThenInclude(x => x.Item)
            .ThenInclude(item => item!.Image)
            .FirstOrDefaultAsync(y => y.Id == id);
    }

    public async Task AddComponentAsync(Element componentToCreate)
    {
        componentToCreate.Kit = null;
        componentToCreate.Item = null;
        _dbContext.Add(componentToCreate);
        await _dbContext.SaveChangesAsync();
    }

    public Task<Element?> GetElementAsync(int kitId, int itemId)
    {
        return _dbContext.Set<Element>()
            .Include(elem => elem.Kit)
            .ThenInclude(kit => kit!.Image)
            .Include(elem => elem.Item)
            .ThenInclude(item => item!.Image)
            .Where(x => x.ItemId == itemId && x.KitId == kitId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateElementAsync(Element element)
    {
        var elementToEdit = await _dbContext.SetElements.FindAsync() ??
                            throw new ResourceNotFoundException(entityName: nameof(Element));
        elementToEdit.PricePerItem = elementToEdit.PricePerItem;
        elementToEdit.ItemsAmount = elementToEdit.ItemsAmount;

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteElementAsync(int kitId, int itemId)
    {
        var elemToDelete = await GetElementAsync(kitId, itemId);
        if (elemToDelete is null) throw new ResourceNotFoundException(nameof(Element));
        var kit = await GetFullKitWithMembershipsByIdAsync(kitId) ??
                  throw new ResourceNotFoundException(entityName: nameof(Kit));

        _dbContext.Remove(elemToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public Task<int> GetElementCount(int kitId)
    {
        return _dbContext.SetElements
            .AsNoTracking()
            .CountAsync(x => x.KitId == kitId);
    }

    public override async Task<Kit> UpdateAsync(Kit entityToUpdate)
    {
        var originalEntity = await _dbContext.Kits
                                 .Include(Kit => Kit.Image)
                                 .FirstOrDefaultAsync(kit => kit.Id == entityToUpdate.Id) ??
                             throw new ResourceNotFoundException(entityName: nameof(Kit));
        var oldOrderNo = originalEntity.OrderNo;

        originalEntity.Name = entityToUpdate.Name;
        originalEntity.Description = entityToUpdate.Description;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;

        var strategy = _dbContext.Database.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            if (oldOrderNo.HasValue) await BulkUpdateOrderAsync(oldOrderNo.Value);
            await _dbContext.SaveChangesAsync();
        });
        return originalEntity;
    }

    public override async Task DeleteAsync(Kit entityToDelete)
    {
        if (!await ExistsAsync(entityToDelete)) throw new ResourceNotFoundException(entityName: nameof(Kit));
        await _dbContext.SetElements.Where(x => x.KitId == entityToDelete.Id)
            .ExecuteDeleteAsync();
        _dbContext.Kits.Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<decimal> GetPricesOfKitComponents(int kitId)
    {
        return _dbContext.Set<Element>()
            .AsNoTracking()
            .Where(x => x.KitId == kitId)
            .Select(x => x.PricePerItem * x.ItemsAmount);
    }
}