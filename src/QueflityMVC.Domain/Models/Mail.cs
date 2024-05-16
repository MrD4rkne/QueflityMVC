namespace QueflityMVC.Domain.Models;

public class Mail
{
    public int Id { get; set; }

    public string Subject { get; set; }

    public string Body { get; set; }

    public string Recipient { get; set; }

    public DateTime SentAt { get; set; }
}