using Microsoft.EntityFrameworkCore;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Domain.Common
{
    public class BaseBuyableEntity : BaseEntity
    {
        public bool ShouldBeShown { get; set; }

        [Precision(14, 2)]
        public decimal Price { get; set; }

    }
}
