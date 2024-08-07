﻿using QueflityMVC.Application.ViewModels.Other;

namespace QueflityMVC.Application.ViewModels.User;

public record UserClaimsVm
{
    public required string UserId { get; set; }

    public string? Username { get; set; }

    public required bool IsEnabled { get; set; }

    public required List<ClaimForSelectionVm> AllClaims { get; set; }

    public required List<string> AssignedClaimsIds { get; set; }

    public bool CanCallerManage { get; set; }
}