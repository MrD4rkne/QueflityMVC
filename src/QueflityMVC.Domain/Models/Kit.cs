namespace QueflityMVC.Domain.Models;

public class Kit : Product
{
    public string? Description { get; set; }

    public override decimal Price
    {
        get => Elements?.Sum(x => x.ItemsAmount * x.PricePerItem) ?? 0;
        protected set
        {
            /* No setter needed, as it is calculated */
        }
    }

    public ICollection<Element>? Elements { get; set; }
}