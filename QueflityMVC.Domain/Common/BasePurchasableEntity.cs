using Microsoft.EntityFrameworkCore;

namespace QueflityMVC.Domain.Common
{
    public class BasePurchasableEntity : BaseEntity
    {
        public bool ShouldBeShown { get; set; }

        [Precision(14, 2)]
        public decimal Price { get; set; }

    }
}
