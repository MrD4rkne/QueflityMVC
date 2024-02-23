using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Application.ViewModels.Purchasable;

namespace QueflityMVC.Application.Interfaces;
public interface IPurchasableEntityService
{
    Task<List<PurchasableVM>> GetEnitiesOrderVM();
}
