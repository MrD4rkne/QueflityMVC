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
        Task DisableUser(string userToDisableId);

        Task<bool> DoesUserExist(string userId);

        Task<ApplicationUser?> GetUserById(string userId);

        Task EnableUser(string userToEnableId);

        IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter);
    }
}
