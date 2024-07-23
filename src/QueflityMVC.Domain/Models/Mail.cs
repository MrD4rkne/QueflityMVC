namespace QueflityMVC.Infrastructure.Abstraction.Model;

public class Mail
{
    public string RecipientName { get; set; }

    public string RecipientEmail { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}