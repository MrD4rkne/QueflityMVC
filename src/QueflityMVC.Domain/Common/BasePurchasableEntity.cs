using System.Runtime.InteropServices.Marshalling;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Common;

public class BasePurchasableEntity : BaseEntity
{
    public required string Name { get; set; }

    public bool ShouldBeShown { get; set; }

    [Precision(14, 2)]
    public required decimal Price { get; set; }

    public required int? ImageId { get; set; }

    public virtual Image? Image { get; set; }

    public uint? OrderNo { get; set; }
}