using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Item : BasePurchasableEntity
{
    public int CategoryId { get; set; }

    public Category? Category { get; set; }

    public ICollection<Element>? SetElements { get; set; }

    public ICollection<Kit>? Kits { get; set; }

    public ICollection<Component>? Components { get; set; }
}