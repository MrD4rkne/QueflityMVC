using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class KitRepository(Context dbContext) : BaseRepository<Kit>(dbContext), IKitRepository
{
    public override Task<Kit?> GetByIdAsync(int entityId)
    {
        return DbContext.Kits
            .AsNoTracking()
            .Include(kit => kit.Image)
            .FirstOrDefaultAsync(kit => kit.Id == entityId);
    }

    public async Task<IQueryable<int>> GetComponenetsIdsForSet(int kitId)
    {
        var set = await GetFullKitWithMembershipsByIdAsync(kitId) ?? throw new ResourceNotFoundException();
        var componentsIds = GetKitComponents(kitId).Select(x => x.ItemId);
        return componentsIds;
    }

    public IQueryable<Element> GetKitComponents(int kitId)
    {
        return DbContext.SetElements
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
        return DbContext.Set<Kit>()
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
        DbContext.Add(componentToCreate);
        await DbContext.SaveChangesAsync();
        await UpdateKitPriceAsync(componentToCreate.KitId);
    }

    public async Task UpdateKitPriceAsync(int kitId)
    {
        var kit = await GetByIdAsync(kitId) ?? throw new ResourceNotFoundException(entityName: nameof(Kit));
        var componentsPrices = GetPricesOfKitComponents(kitId);
        var sumOfComponentPrices = componentsPrices.Sum();
        kit.Price = sumOfComponentPrices;
        await UpdateAsync(kit);
    }

    public Task<Element?> GetElementAsync(int kitId, int itemId)
    {
        return DbContext.Set<Element>()
            .Include(elem => elem.Kit)
            .ThenInclude(kit => kit!.Image)
            .Include(elem => elem.Item)
            .ThenInclude(item => item!.Image)
            .Where(x => x.ItemId == itemId && x.KitId == kitId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateElementAsync(Element element)
    {
        var elementToEdit = await DbContext.SetElements.FindAsync() ?? throw new ResourceNotFoundException(entityName: nameof(Element));
        elementToEdit.PricePerItem = elementToEdit.PricePerItem;
        elementToEdit.ItemsAmount = elementToEdit.ItemsAmount;
        
        await DbContext.SaveChangesAsync();
        await UpdateKitPriceAsync(elementToEdit.KitId);
    }

    public async Task DeleteElementAsync(int kitId, int itemId)
    {
        var elemToDelete = await GetElementAsync(kitId, itemId);
        if (elemToDelete is null) throw new ResourceNotFoundException(nameof(Element));

        DbContext.Remove(elemToDelete);
        await UpdateKitPriceAsync(elemToDelete.KitId);
        await DbContext.SaveChangesAsync();
    }

    public override async Task<Kit> UpdateAsync(Kit entityToUpdate)
    {
        var originalEntity = await DbContext.Kits
                                 .Include(Kit=>Kit.Image)
                                 .FirstOrDefaultAsync(kit=> kit.Id==entityToUpdate.Id) ??
                             throw new ResourceNotFoundException(entityName: nameof(Kit));
        originalEntity.Name = entityToUpdate.Name;
        originalEntity.Description = entityToUpdate.Description;
        originalEntity.Price = entityToUpdate.Price;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;
        
        await DbContext.SaveChangesAsync();
        return originalEntity;
    }

    public override async Task DeleteAsync(Kit entityToDelete)
    {
        if (!await ExistsAsync(entityToDelete)) throw new ResourceNotFoundException(entityName: nameof(Kit));
        await DbContext.SetElements.Where(x => x.KitId == entityToDelete.Id)
            .ExecuteDeleteAsync();
        DbContext.Kits.Remove(entityToDelete);
        await DbContext.SaveChangesAsync();
    }

    public IQueryable<decimal> GetPricesOfKitComponents(int kitId)
    {
        return DbContext.Set<Element>()
            .AsNoTracking()
            .Where(x => x.KitId == kitId)
            .Select(x => x.PricePerItem * x.ItemsAmount);
    }
}