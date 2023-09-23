using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Errors.Identity;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task DisableUser(string userToDisableId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userToDisableId);        
            
            bool doesUserExists = await _userRepository.DoesUserExist(userToDisableId);
            if(!doesUserExists)
            {
                throw new EntityNotFoundException(entityName: nameof(ApplicationUser));
            }

            await _userRepository.DisableUser(userToDisableId);
        }

        public async Task EnableUser(string userToEnableId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userToEnableId);

            bool doesUserExists = await _userRepository.DoesUserExist(userToEnableId);
            if (!doesUserExists)
            {
                throw new EntityNotFoundException(entityName: nameof(ApplicationUser));
            }

            await _userRepository.EnableUser(userToEnableId);
        }

        public async Task<ListUsersVM> GetFilteredList(ListUsersVM listUsersVM)
        {
            ArgumentNullException.ThrowIfNull(listUsersVM);
            ArgumentNullException.ThrowIfNull(listUsersVM.Pagination);

            IQueryable<ApplicationUser> matchingUsers = _userRepository.GetFilteredUsers(listUsersVM.UserNameFilter);
            listUsersVM.Pagination = await matchingUsers.Paginate<ApplicationUser, UserForListVM>(listUsersVM.Pagination, _mapper.ConfigurationProvider);

            return listUsersVM;
        }
    }
}
