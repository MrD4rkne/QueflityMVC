using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories;

public class KitRepository : BaseRepository<Kit>, IKitRepository
{
    public KitRepository(Context dbContext) : base(dbContext)
    {
    }

    public override Task<Kit?> GetByIdAsync(int entityId)
    {
        return _dbContext.Kits
            .Include(kit => kit.Image)
            .FirstOrDefaultAsync(kit => kit.Id == entityId);
    }

    public async Task<IQueryable<int>> GetComponenetsIdsForSet(int kitId)
    {
        var set = await GetFullKitWithMembershipsByIdAsync(kitId);
        if (set is null)
        {
            throw new ResourceNotFoundException();
        }

        var componentsIds = GetKitComponents(kitId).Select(x => x.ItemId);
        return componentsIds;
    }

    public IQueryable<Element> GetKitComponents(int kitId)
    {
        return _dbContext.SetElements.Where(x => x.KitId == kitId);
    }

    public IQueryable<Kit> GetFilteredKits(string? searchName = default, int? itemId = null)
    {
        var itemsSource = GetAll();
        if (itemId.HasValue)
        {
            itemsSource = itemsSource.Where(x => x.Elements.Any(y => y.ItemId == itemId));
        }
        if (!string.IsNullOrEmpty(searchName))
        {
            itemsSource = itemsSource.Where(x => x.Name.StartsWith(searchName));
        }
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

    public IQueryable<decimal> GetPricesOfKitComponents(int kitId)
    {
        return _dbContext.Set<Element>()
            .Where(x => x.KitId == kitId)
            .Select(x => x.PricePerItem * x.ItemsAmmount);
    }

    public async Task AddComponentAsync(Element componentToCreate)
    {
        componentToCreate.Kit = null;
        componentToCreate.Item = null;
        _dbContext.Add(componentToCreate);
        await _dbContext.SaveChangesAsync();
        await UpdateKitPriceAsync(componentToCreate.KitId);
    }

    public async Task UpdateKitPriceAsync(int kitId)
    {
        Kit? kit = await GetByIdAsync(kitId) ?? throw new EntityNotFoundException(entityName: nameof(Kit));

        var componentsPrices = GetPricesOfKitComponents(kitId);
        decimal sumOfComponentPrices = componentsPrices.Sum();
        kit.Price = sumOfComponentPrices;
        await UpdateAsync(kit);
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
        Element? elementToEdit = await GetElementAsync(element.KitId, element.ItemId);
        if (elementToEdit is null)
        {
            throw new EntityNotFoundException(entityName: nameof(Element));
        }

        elementToEdit.PricePerItem = elementToEdit.PricePerItem;
        elementToEdit.ItemsAmmount = elementToEdit.ItemsAmmount;

        _dbContext.Entry(elementToEdit).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();

        await UpdateKitPriceAsync(elementToEdit.KitId);
    }

    public async Task DeleteElementAsync(int kitId, int itemId)
    {
        Element? elemToDelete = await GetElementAsync(kitId, itemId);
        if (elemToDelete is null)
        {
            throw new EntityNotFoundException(nameof(Element));
        }

        _dbContext.Remove(elemToDelete);
        await UpdateKitPriceAsync(elemToDelete.KitId);
        await _dbContext.SaveChangesAsync();
    }

    public override async Task<Kit> UpdateAsync(Kit entityToUpdate)
    {
        Kit originalEntity = await GetByIdAsync(entityToUpdate.Id) ?? throw new EntityNotFoundException(entityName: nameof(Kit));
        originalEntity.Name = entityToUpdate.Name;
        originalEntity.Description = entityToUpdate.Description;
        originalEntity.Price = entityToUpdate.Price;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;

        _dbContext.Entry(originalEntity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return originalEntity;
    }

    public override async Task DeleteAsync(Kit entityToDelete)
    {
        if (!await ExistsAsync(entityToDelete))
        {
            throw new EntityNotFoundException(entityName: nameof(Kit));
        }
        await _dbContext.SetElements.
            Where(x => x.KitId == entityToDelete.Id)
            .ExecuteDeleteAsync();
        _dbContext.Kits.Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public IQueryable<BasePurchasableEntity> GetVisibileEntities()
    {
        return _dbContext.Kits
            .Where(x => x.ShouldBeShown);
    }
}