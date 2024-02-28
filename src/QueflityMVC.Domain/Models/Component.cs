using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Component : BaseEntity
{
    public  string Name { get; set; }

    public ICollection<Item>? Items { get; set; }
}