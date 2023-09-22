using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected Context _dbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserRepository(Context dbContext, UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IQueryable<IdentityUser> GetFilteredUsers(string? userNameFilter)
        {
            IQueryable<IdentityUser> matchingUsers = _userManager.Users;
            if (!string.IsNullOrEmpty(userNameFilter))
            {
                matchingUsers = matchingUsers.Where(user => user.UserName != null)
                    .Where(x => x.UserName!.Contains(userNameFilter));
            }

            return matchingUsers;

            _userManager.
        }
    }
}
