using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Persistence.Common;

public abstract class BaseRepository<T>(Context dbContext) : IBaseRepository<T>
    where T : BaseEntity
{
    protected readonly Context DbContext = dbContext;

    public virtual async Task<int> AddAsync(T entityToAdd)
    {
        DbContext.Set<T>().Add(entityToAdd);
        await DbContext.SaveChangesAsync();

        return entityToAdd.Id;
    }

    public virtual async Task DeleteAsync(int entityToDeleteId)
    {
        var entityToDelete = await GetByIdAsync(entityToDeleteId) ??
                             throw new ResourceNotFoundException(entityName: nameof(T));
        await DeleteAsync(entityToDelete);
    }

    public virtual async Task DeleteAsync(T entityToDelete)
    {
        if (!await ExistsAsync(entityToDelete)) throw new ResourceNotFoundException(entityName: nameof(T));

        DbContext.Set<T>().Remove(entityToDelete);
        await DbContext.SaveChangesAsync();
    }

    public virtual async Task<T?> UpdateAsync(T entityToUpdate)
    {
        var entity = await GetByIdAsync(entityToUpdate.Id) ??
                     throw new ResourceNotFoundException(entityName: nameof(T));
        if (DbContext.Entry(entity).State == EntityState.Detached) DbContext.Set<T>().Attach(entity);

        DbContext.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        await DbContext.SaveChangesAsync();
        return await GetByIdAsync(entityToUpdate.Id);
    }

    public virtual Task<bool> ExistsAsync(T entityToCheck)
    {
        return ExistsAsync(entityToCheck.Id);
    }

    public virtual async Task<bool> ExistsAsync(int entityId)
    {
        return await GetByIdAsync(entityId) != null;
    }

    public virtual Task<T?> GetByIdAsync(int entityId)
    {
        return DbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(ent => ent.Id == entityId);
    }

    public IQueryable<T> GetAll()
    {
        return DbContext.Set<T>()
            .AsNoTracking();
    }
}