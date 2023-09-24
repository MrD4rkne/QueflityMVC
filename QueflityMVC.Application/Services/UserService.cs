using AutoMapper;
using AutoMapper.QueryableExtensions;
using QueflityMVC.Application.Common.Pagination;
using QueflityMVC.Application.Errors.Common;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Role;
using QueflityMVC.Application.ViewModels.User;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;

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
            if (!doesUserExists)
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

        public async Task<UserRolesVM> GetUsersRolesVM(string userId)
        {
            ArgumentException.ThrowIfNullOrEmpty(userId);

            var user = await _userRepository.GetUserById(userId);
            if (user is null)
            {
                throw new EntityNotFoundException();
            }

            var allRoles = _userRepository.GetAllRoles()
                .ProjectTo<RoleForSelectionVM>(_mapper.ConfigurationProvider);

            var assignedRolesIds = await _userRepository.GetAssignedRolesIds(userId);

            UserRolesVM userRolesVM = new()
            {
                UserId = userId,
                Username = user.UserName,
                IsEnabled = user.IsEnabled,
                AllRoles = allRoles.ToList(),
                AssignedRolesIds = assignedRolesIds.ToList()
            };
            return userRolesVM;
        }

        public async Task UpdateUserRoles(UserRolesVM userRolesVM)
        {
            ArgumentNullException.ThrowIfNull(userRolesVM);
            ArgumentNullException.ThrowIfNullOrEmpty(userRolesVM.UserId);

            await Parallel.ForEachAsync(userRolesVM.AllRoles,
                async (role, cs) => { await UpdateRoleMembership(role, userRolesVM.UserId); });
        }

        private Task UpdateRoleMembership(RoleForSelectionVM role, string userId)
        {
            if (role is null)
                return Task.CompletedTask;

            if (role.IsSelected)
            {
                return _userRepository.AddToRole(userId, role.Id);
            }
            else
            {
                return _userRepository.RemoveFromRole(userId, role.Id);
            }
        }
    }
}
