using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Ingredient : BaseEntity
{
    public required string Name { get; set; }

    public ICollection<Item>? Items { get; set; }
}