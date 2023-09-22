using QueflityMVC.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.Interfaces
{
    public interface IUserService
    {
        void DeleteUser(string userId);
        Task<ListUsersVM> GetFilteredList(ListUsersVM listUsersVM);
    }
}
