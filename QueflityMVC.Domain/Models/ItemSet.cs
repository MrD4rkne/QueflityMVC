using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class ItemSet : BaseBuyableEntity
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int? ItemSetImageId { get; set; }

        public virtual ItemSetImage? Image { get; set; }

        public virtual ICollection<SetMembership> SetMemberships { get; set; }
    }
}
