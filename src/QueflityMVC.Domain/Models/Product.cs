using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public abstract class Product : BaseEntity
{
    public string Name { get; set; }

    public bool ShouldBeShown { get; set; }

    public virtual decimal Price { get; protected set; }

    public int? ImageId { get; set; }

    public Image? Image { get; set; }

    public uint? OrderNo { get; set; }
}