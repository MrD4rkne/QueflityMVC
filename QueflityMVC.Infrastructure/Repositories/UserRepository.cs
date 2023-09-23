﻿using Microsoft.AspNetCore.Identity;
using QueflityMVC.Domain.Errors;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
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
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(Context dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IQueryable<ApplicationUser> GetFilteredUsers(string? userNameFilter)
        {
            IQueryable<ApplicationUser> matchingUsers = _userManager.Users;
            if (!string.IsNullOrEmpty(userNameFilter))
            {
                matchingUsers = matchingUsers.Where(user => user.UserName != null)
                    .Where(x => x.UserName!.Contains(userNameFilter));
            }

            return matchingUsers;
        }

        public async Task DisableUser(string userId)
        {
            var userToDisable = await GetUserById(userId);
            if(userToDisable is null)
            {
                throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
            }

            userToDisable.IsEnabled = false;
            await _userManager.UpdateAsync(userToDisable);
            await _userManager.UpdateSecurityStampAsync(userToDisable);
        }

        public Task<ApplicationUser?> GetUserById(string userId)
        {
            return _userManager.FindByIdAsync(userId);
        }

        public async Task<bool> DoesUserExist(string userId)
        {
            ApplicationUser? allegedUser = await _userManager.FindByIdAsync(userId);
            return allegedUser is not null;
        }

        public async Task EnableUser(string userToEnableId)
        {
            var userToEnable = await GetUserById(userToEnableId);
            if (userToEnable is null)
            {
                throw new ResourceNotFoundException(entityName: nameof(ApplicationUser));
            }

            userToEnable.IsEnabled = true;
            await _userManager.UpdateAsync(userToEnable);
        }
    }
}
