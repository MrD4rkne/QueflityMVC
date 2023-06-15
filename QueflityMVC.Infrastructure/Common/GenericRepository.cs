using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Infrastructure.Common
{
    public abstract class GenericRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected Context _dbContext;

        public GenericRepository(Context dbContext){
            _dbContext = dbContext;
        }

        public abstract DbSet<T> Table();

        public virtual int Add(T entityToAdd)
        {
            if (entityToAdd == null)
                throw new ArgumentNullException("Entity cannot be null");
            
            Table().Add(entityToAdd);
            _dbContext.SaveChanges();

            return entityToAdd.Id;
        }

        public virtual void Delete(int entityToDeleteId)
        {
            var entityToDelete = GetById(entityToDeleteId);

            Delete(entityToDelete);
        }

        public virtual void Delete(T entityToDelete)
        {
            if (!Exists(entityToDelete))
                throw new ArgumentException("Entity does not exist!");

            Table().Remove(entityToDelete); 
            _dbContext.SaveChanges();
        }

        public virtual T Update(T entityToUpdate)
        {
            if (!Exists(entityToUpdate))
                throw new ArgumentException("Entity does not exist!");

            Table().Update(entityToUpdate);
            _dbContext.SaveChanges();

            return GetById(entityToUpdate.Id)!;
        }

        public virtual bool Exists(T entityToCheck)
        {
            if (entityToCheck == null)
                throw new ArgumentNullException("Entity cannot be null");

            return Exists(entityToCheck.Id);
        }

        public virtual bool Exists(int entityId)
        {
            return GetById(entityId) != null;
        }

        public virtual T? GetById(int entityId)
        {
            return Table().FirstOrDefault(ent => ent.Id == entityId);
        }
    }
}
