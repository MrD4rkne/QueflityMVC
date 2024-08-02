using QueflityMVC.Domain.Common;

namespace QueflityMVC.Domain.Models;

public class Conversation : BaseEntity
{
    public string Title { get; set; }

    public int ProductId { get; set; }

    public Product Product { get; set; }

    public Guid UserId { get; set; }

    public ApplicationUser User { get; set; }

    public bool IsClosed { get; set; }

    public List<Message> Messages { get; set; }
}