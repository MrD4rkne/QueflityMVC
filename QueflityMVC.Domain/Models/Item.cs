using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models
{
    public class Item : BasePurchasableEntity
    {
        public string Name { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int? ItemImageId { get; set; }

        public virtual ItemImage? Image { get; set; }

        public virtual ICollection<Element>? SetElements { get; set; }

        public virtual ICollection<Ingredient>? Ingredients { get; set; }
    }
}
