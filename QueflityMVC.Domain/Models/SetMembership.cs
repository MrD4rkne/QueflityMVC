using Microsoft.EntityFrameworkCore;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Domain.Models
{
    public class SetMembership : BaseEntity
    {
        public uint ItemsAmmount { get; set; }

        [Precision(14, 2)]
        public decimal PricePerItem { get; set; }

        public int ItemSetId { get; set; }

        public virtual ItemSet ItemSet { get; set; }

        public int ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
