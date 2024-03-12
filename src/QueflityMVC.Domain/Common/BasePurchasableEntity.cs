using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Common;

public class BasePurchasableEntity : BaseEntity
{
    public string Name { get; set; }

    public bool ShouldBeShown { get; set; }

    [Precision(14, 2)] public decimal Price { get; set; }

    public int? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public uint? OrderNo { get; set; }
}