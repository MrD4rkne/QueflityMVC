using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.Emails;

public class ResetPasswordEmail
{
    public string Email { get; set; }

    public ApplicationUser User { get; set; }

    public string Url { get; set; }
}