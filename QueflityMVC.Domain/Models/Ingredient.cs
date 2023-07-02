using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Domain.Models
{
    public class Ingredient : BaseEntity
    {
        public string Name { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
