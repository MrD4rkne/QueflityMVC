using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<IdentityUser> GetFilteredUsers(string? userNameFilter);
    }
}
