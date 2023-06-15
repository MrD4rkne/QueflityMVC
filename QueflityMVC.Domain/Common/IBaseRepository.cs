using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Common
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        int Add(T entityToAdd);

        void Delete(int entityToDeleteId);

        void Delete(T entityToDelete);

        T Update(T entityToUpdate);

        bool Exists(T entityToCheck);

        bool Exists(int entityId);

        T? GetById(int entityId);
    }
}
