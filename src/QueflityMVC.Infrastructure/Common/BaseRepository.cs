using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Infrastructure.Common;

public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected Context _dbContext;

    public BaseRepository(Context dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<int> AddAsync(T entityToAdd)
    {
        _dbContext.Set<T>().Add(entityToAdd);
        await _dbContext.SaveChangesAsync();

        return entityToAdd.Id;
    }

    public virtual async Task DeleteAsync(int entityToDeleteId)
    {
        var entityToDelete = (await GetByIdAsync(entityToDeleteId)) ?? throw new EntityNotFoundException(entityName: nameof(T));
        await DeleteAsync(entityToDelete);
    }

    public virtual async Task DeleteAsync(T entityToDelete)
    {
        if (!await ExistsAsync(entityToDelete))
        {
            throw new EntityNotFoundException(entityName: nameof(T));
        }

        _dbContext.Set<T>().Remove(entityToDelete);
        await _dbContext.SaveChangesAsync();
    }

    public virtual async Task<T> UpdateAsync(T entityToUpdate)
    {
        var entity = await GetByIdAsync(entityToUpdate.Id) ?? throw new EntityNotFoundException(entityName: nameof(T));
        if (_dbContext.Entry(entity).State == EntityState.Detached)
        {
            _dbContext.Set<T>().Attach(entity);
        }
        _dbContext.Entry(entity).CurrentValues.SetValues(entityToUpdate);
        await _dbContext.SaveChangesAsync();
        return await GetByIdAsync(entityToUpdate.Id);
    }

    public virtual Task<bool> ExistsAsync(T entityToCheck)
    {
        if (entityToCheck is null)
            throw new ArgumentNullException("Entity cannot be null");

        return ExistsAsync(entityToCheck.Id);
    }

    public virtual async Task<bool> ExistsAsync(int entityId)
    {
        return (await GetByIdAsync(entityId)) != null;
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