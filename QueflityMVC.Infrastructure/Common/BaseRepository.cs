using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Infrastructure.Common
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected Context _dbContext;

        public BaseRepository(Context dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual int Add(T entityToAdd)
        {
            _dbContext.Set<T>().Add(entityToAdd);
            _dbContext.SaveChanges();

            return entityToAdd.Id;
        }

        public virtual void Delete(int entityToDeleteId)
        {
            var entityToDelete = GetById(entityToDeleteId);
            if (entityToDelete is null)
            {
                throw new EntityNotFoundException(entityName: nameof(T));
            }

            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (!Exists(entityToDelete))
            {
                throw new EntityNotFoundException(entityName: nameof(T));
            }

            _dbContext.Set<T>().Remove(entityToDelete);
            _dbContext.SaveChanges();
        }

        public virtual T Update(T entityToUpdate)
        {
            if (!Exists(entityToUpdate))
            {
                throw new EntityNotFoundException(entityName: nameof(T)); throw new ArgumentException("Entity does not exist!");
            }

            if (_dbContext.Entry(entityToUpdate) is EntityEntry<T> originalEntity)
            {
                originalEntity.CurrentValues.SetValues(entityToUpdate);
            }
            else
            {
                _dbContext.Attach(entityToUpdate);
                _dbContext.Entry(entityToUpdate).State = EntityState.Modified;

            }

            _dbContext.SaveChanges();

            return GetById(entityToUpdate.Id)!;
        }

        public virtual bool Exists(T entityToCheck)
        {
            if (entityToCheck is null)
                throw new ArgumentNullException("Entity cannot be null");

            return Exists(entityToCheck.Id);
        }

        public virtual bool Exists(int entityId)
        {
            return GetById(entityId) != null;
        }

        public virtual T? GetById(int entityId)
        {
            return _dbContext.Set<T>().FirstOrDefault(ent => ent.Id == entityId);
        }

        public IQueryable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }
    }
}
