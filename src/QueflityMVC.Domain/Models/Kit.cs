using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Domain.Models;

public class Kit : Product
{
    public string? Description { get; set; }
    
    public override decimal Price
    {
        get => Elements.Sum(e => e.ItemsAmount * e.PricePerItem);
        protected set { /* No setter needed, as it is calculated */ }
    }

    public ICollection<Element> Elements { get; set; }
}