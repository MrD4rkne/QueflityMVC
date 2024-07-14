using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Element : BaseEntity
{
    public uint ItemsAmount { get; set; }

    [Precision(14, 2)] public decimal PricePerItem { get; set; }

    public int ItemId { get; set; }

    public Item? Item { get; set; }

    public int KitId { get; set; }

    public Kit? Kit { get; set; }
}