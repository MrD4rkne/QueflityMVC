using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.Purchasable;
public record EditOrderVM
{
    public required List<PurchasableVM> PurchasablesVMs { get; set; }
}
