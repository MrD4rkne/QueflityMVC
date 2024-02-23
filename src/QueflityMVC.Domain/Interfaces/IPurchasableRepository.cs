using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;
public interface IPurchasableRepository
{
    IQueryable<BasePurchasableEntity> GetVisibileEntities();
}
