using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Interfaces;
public interface IPurchasableRepository
{
    Task<bool> AreTheseAllVisiblePurchasablesAsync(List<BasePurchasableEntity> purchasableModels);
    IQueryable<BasePurchasableEntity> GetVisibileEntities();
    Task UpdatePurchasablesOrderAsync(List<BasePurchasableEntity> purchasableModels);
}
