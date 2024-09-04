namespace QueflityMVC.Domain.Models;

public class EmailConfirmation
{
    public string Email { get; set; }

    public ApplicationUser User { get; set; }

    public string Url { get; set; }
}