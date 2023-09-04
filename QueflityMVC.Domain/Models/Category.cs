using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Domain.Models
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }

        public virtual ICollection<Item>? Items { get; set; }
    }
}
