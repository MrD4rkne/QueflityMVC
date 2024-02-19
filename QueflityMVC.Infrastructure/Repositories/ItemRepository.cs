using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(Context dbContext) : base(dbContext)
    {
    }

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
        {
            entitiesSource = entitiesSource.Where(x => x.Name.StartsWith(nameFilter));
        }
        if (categoryId.HasValue)
        {
            entitiesSource = entitiesSource.Where(x => x.CategoryId == categoryId);
        }

        return entitiesSource;
    }

    public Task<Item?> GetItemWithIngredientsByIdAsync(int itemId)
    {
        return _dbContext.Items
            .Include(x => x.Ingredients)
            .Include(it => it.Image)
            .FirstOrDefaultAsync(x => x.Id == itemId);
    }

    public async Task UpdateIngredientsAsync(int itemId, List<Ingredient> ingredients)
    {
        var item = await GetItemWithIngredientsByIdAsync(itemId);
        if (item is null)
        {
            throw new ResourceNotFoundException();
        }

        if (ingredients is not null)
        {
            item.Ingredients = ingredients;
        }

        await UpdateAsync(item);
    }

    public override async Task<Item> UpdateAsync(Item entityToUpdate)
    {
        Item originalEntity = await GetItemWithIngredientsByIdAsync(entityToUpdate.Id) ?? throw new EntityNotFoundException(entityName: nameof(Item));
        if (_dbContext.Entry(originalEntity).State == EntityState.Detached)
        {
            _dbContext.Attach(originalEntity);
        }
        originalEntity.Name = entityToUpdate.Name;
        originalEntity.CategoryId = entityToUpdate.CategoryId;
        originalEntity.Price = entityToUpdate.Price;
        originalEntity.ShouldBeShown = entityToUpdate.ShouldBeShown;
        originalEntity.Image.AltDescription = entityToUpdate.Image.AltDescription;
        originalEntity.Image.FileUrl = entityToUpdate.Image.FileUrl;
        if(entityToUpdate.Ingredients is not null)
        {
            originalEntity.Ingredients = entityToUpdate.Ingredients;
        }
        _dbContext.Entry(originalEntity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return originalEntity;
    }

    public Task<bool> IsItemAPartOfAnyKitAsync(int id)
    {
        return _dbContext.SetElements
            .AnyAsync(x => x.ItemId == id);
    }
}