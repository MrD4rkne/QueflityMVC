using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Element : BaseEntity
{
    public required uint ItemsAmmount { get; set; }

    [Precision(14, 2)]
    public required decimal PricePerItem { get; set; }

    public required int ItemId { get; set; }

    public Item? Item { get; set; }

    public required int KitId { get; set; }

    public Kit? Kit { get; set; }

}