using Microsoft.AspNetCore.Identity;

namespace QueflityMVC.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsEnabled { get; set; }

        public ApplicationUser()
        {
        }
    }
}