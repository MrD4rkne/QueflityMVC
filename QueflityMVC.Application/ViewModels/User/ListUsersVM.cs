using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Application.ViewModels.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueflityMVC.Application.ViewModels.User
{
    public record ListUsersVM
    {
        public required PaginationVM<UserForListVM> Pagination { get; set; }

        public string? UserNameFilter { get; set; }
    }
}
