namespace QueflityMVC.Domain.Models;

public class Message
{
    public int Id { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string Recipient { get; set; }

    public DateTime SentAt { get; set; }

    public int PurchasableId { get; set; }
}