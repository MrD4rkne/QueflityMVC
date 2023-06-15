using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
