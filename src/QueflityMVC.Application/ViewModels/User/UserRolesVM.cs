using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.ViewModels.User;

public record UserRolesVm
{
    public required Guid UserId { get; set; }

    public string? Username { get; set; }

    public required bool IsEnabled { get; set; }

    public required List<RoleForSelectionVm> AllRoles { get; set; }

    public required List<string> AssignedRolesNames { get; set; }

    public bool CanCallerManage { get; set; }
}