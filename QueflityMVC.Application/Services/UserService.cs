using AutoMapper;
using Microsoft.AspNetCore.Identity;
using QueflityMVC.Application.Common.Pagination;
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

        public async Task<ListUsersVM> GetFilteredList(ListUsersVM listUsersVM)
        {
            ArgumentNullException.ThrowIfNull(listUsersVM);
            ArgumentNullException.ThrowIfNull(listUsersVM.Pagination);

            _userManager.Users
            IQueryable<IdentityUser> matchingUsers = _userRepository.GetFilteredUsers(listUsersVM.UserNameFilter);
            listUsersVM.Pagination = await matchingUsers.Paginate<IdentityUser, UserForListVM>(listUsersVM.Pagination, _mapper.ConfigurationProvider);

            return listUsersVM;
        }
    }
}
