using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Kit : BasePurchasableEntity
{
    public string? Description { get; set; }

    public ICollection<Element> Elements { get; set; } = new List<Element>();
}