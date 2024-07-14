﻿using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ItemRepository(Context dbContext) : BasePurchasableRepository<Item>(dbContext), IItemRepository
{
    public override Task<Item?> GetByIdAsync(int entityId)
    {
        return _dbContext.Items
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
        return _dbContext.Items
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

    public Task<uint?> GetOrderNoByIdAsync(int itemId)
    {
        return _dbContext.Items
            .AsNoTracking()
            .Where(x => x.Id == itemId)
            .Select(x => x.OrderNo)
            .FirstOrDefaultAsync();
    }

    public override async Task<Item> UpdateAsync(Item entityToUpdate)
    {
        var originalEntity = await dbContext.Items
                                 .Include(Item => Item.Image)
                                 .Include(Item=> Item.Components)
                                 .FirstOrDefaultAsync(Item => Item.Id == entityToUpdate.Id) 
                             ?? throw new ResourceNotFoundException(entityName: nameof(Item));
        
        uint? oldOrderNo = originalEntity.OrderNo;
        
        originalEntity.Name = entityToUpdate.Name;
        originalEntity.CategoryId = entityToUpdate.CategoryId;
        originalEntity.SetPrice(entityToUpdate.Price);
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;
        if (entityToUpdate.Components is not null) originalEntity.Components = entityToUpdate.Components;

        await _dbContext.SaveChangesAsync();
        return originalEntity;
    }

    public Task<bool> IsItemAPartOfAnyKitAsync(int id)
    {
        return _dbContext.SetElements
            .AnyAsync(x => x.ItemId == id);
    }
}