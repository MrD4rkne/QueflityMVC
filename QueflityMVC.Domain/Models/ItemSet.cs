using QueflityMVC.Domain.Common;
using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class ItemSet : BaseBuyableEntity
    {
        public string Name { get; set; }

        public int? ItemSetImageId { get; set; }

        public virtual ItemSetImage? ItemSetImage { get; set; }

        public virtual ICollection<SetMembership> SetMemberships { get; set; }
    }
}
