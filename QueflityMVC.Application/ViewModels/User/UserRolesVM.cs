using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.ViewModels.User
{
    public record UserRolesVM
    {
        public required string UserId { get; set; }

        public string? Username { get; set; }

        public required bool IsEnabled { get; set; }

        public required List<RoleForSelectionVM> AllRoles { get; set; }

        public required List<string> AssignedRolesIds { get; set; }

        public bool CanCallerManage { get; set; }
    }
}