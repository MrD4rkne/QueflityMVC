using Microsoft.AspNetCore.Identity;

namespace QueflityMVC.Domain.Models;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole()
    {
        Id = Guid.NewGuid();
    }

    public ApplicationRole(string roleName) : this()
    {
        Name = roleName;
    }
}