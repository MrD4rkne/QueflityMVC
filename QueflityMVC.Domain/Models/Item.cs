using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class Item : BaseBuyableEntity
    {
        public string Name { get; set; }

        public int ItemCategoryId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }

        public int? ItemImageId { get; set; }

        public virtual ItemImage? Image { get; set; }

        public virtual ICollection<SetMembership> SetMemberships { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
