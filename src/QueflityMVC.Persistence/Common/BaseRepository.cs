using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Persistence.Common;

public abstract class BaseRepository<T>(Context dbContext) : IBaseRepository<T>
    where T : BaseEntity
{
    protected Context _dbContext = dbContext;

    public virtual async Task<int> AddAsync(T entityToAdd)
    {
        _dbContext.Set<T>().Add(entityToAdd);
        await _dbContext.SaveChangesAsync();

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

        _dbContext.Set<T>().Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<T?> UpdateAsync(T entityToUpdate)
    {
        var entity = await GetByIdAsync(entityToUpdate.Id) ??
                     throw new ResourceNotFoundException(entityName: nameof(T));
        if (_dbContext.Entry(entity).State == EntityState.Detached) _dbContext.Set<T>().Attach(entity);
        
        _dbContext.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        await _dbContext.SaveChangesAsync();
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
        return _dbContext.Set<T>()
            .AsNoTracking()
            .FirstOrDefaultAsync(ent => ent.Id == entityId);
    }

    public IQueryable<T> GetAll()
    {
        return _dbContext.Set<T>()
            .AsNoTracking();
    }
}