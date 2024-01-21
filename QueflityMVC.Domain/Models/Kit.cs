using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Kit : BasePurchasableEntity
{
    public string Name { get; set; }

    public string? Description { get; set; }

    public int? KitImageId { get; set; }

    public virtual KitImage? Image { get; set; }

    public virtual ICollection<Element> Elements { get; set; } = new List<Element>();
}