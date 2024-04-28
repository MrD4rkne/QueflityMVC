using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ItemRepository(Context dbContext) : BaseRepository<Item>(dbContext), IItemRepository
{
    public override Task<Item?> GetByIdAsync(int entityId)
    {
        return DbContext.Items
            .AsNoTracking()
            .Include(it => it.Image)
            .FirstOrDefaultAsync(it => it.Id == entityId);
    }

    public IQueryable<Item> GetFilteredItems(string? nameFilter, int? categoryId)
    {
        var entitiesSource = GetAll();

        if (!string.IsNullOrEmpty(nameFilter))
            entitiesSource = entitiesSource.Where(x => x.Name.StartsWith(nameFilter));
        if (categoryId.HasValue) entitiesSource = entitiesSource.Where(x => x.CategoryId == categoryId);

        return entitiesSource;
    }

    public Task<Item?> GetItemWithComponentsByIdAsync(int itemId)
    {
        return DbContext.Items
            .AsNoTracking()
            .Include(x => x.Components)
            .Include(it => it.Image)
            .FirstOrDefaultAsync(x => x.Id == itemId);
    }

    public async Task UpdateComponentsAsync(int itemId, List<Component> components)
    {
        var item = await GetItemWithComponentsByIdAsync(itemId);
        if (item is null) throw new ResourceNotFoundException();

        if (components is not null) item.Components = components;

        await UpdateAsync(item);
    }

    public override async Task<Item> UpdateAsync(Item entityToUpdate)
    {
        var originalEntity = await GetItemWithComponentsByIdAsync(entityToUpdate.Id) ??
                             throw new ResourceNotFoundException(entityName: nameof(Item));
        if (DbContext.Entry(originalEntity).State == EntityState.Detached) DbContext.Attach(originalEntity);

        originalEntity.Name = entityToUpdate.Name;
        originalEntity.CategoryId = entityToUpdate.CategoryId;
        originalEntity.Price = entityToUpdate.Price;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;

        if (entityToUpdate.Components is not null) originalEntity.Components = entityToUpdate.Components;

        DbContext.Entry(originalEntity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync();

        return originalEntity;
    }

    public Task<bool> IsItemAPartOfAnyKitAsync(int id)
    {
        return DbContext.SetElements
            .AnyAsync(x => x.ItemId == id);
    }
}