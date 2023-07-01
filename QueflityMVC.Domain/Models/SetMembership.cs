using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class SetMembership : BaseEntity
    {
        public uint ItemsAmount { get; set; }

        public decimal PricePerItem { get; set; }

        public int ItemSetId { get; set; }

        public virtual ItemSet ItemSet { get; set; }

        public int ItemId { get; set; }

        public Item Item { get; set; }
    }
}
