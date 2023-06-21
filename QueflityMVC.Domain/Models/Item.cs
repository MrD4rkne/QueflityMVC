using QueflityMVC.Infrastructure.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Models
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }

        public int ItemCategoryId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }

        public int? ImageId { get; set; }

        public virtual Image? Image { get; set; }

        public virtual ICollection<ItemSet> ItemSets { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set;}
    }
}
