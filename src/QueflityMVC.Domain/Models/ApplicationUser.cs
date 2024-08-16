using Microsoft.AspNetCore.Identity;

namespace QueflityMVC.Domain.Models;

public class ApplicationUser : IdentityUser<Guid>
{
    public bool IsEnabled { get; set; }
}