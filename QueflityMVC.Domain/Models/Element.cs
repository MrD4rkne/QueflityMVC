using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class Element : BaseEntity
    {
        public uint ItemsAmmount { get; set; }

        [Precision(14, 2)]
        public decimal PricePerItem { get; set; }

        public virtual Item? Item { get; set; }

        public int ItemId { get; set; }

        public virtual Kit? Kit { get; set; }

        public int KitId { get; set; }
    }
}