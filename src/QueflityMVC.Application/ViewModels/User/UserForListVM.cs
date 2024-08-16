using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.User;

public record UserForListVm : IMapFrom<ApplicationUser>
{
    public required Guid Id { get; set; }

    public required string? UserName { get; set; }

    public required string? Email { get; set; }

    public required bool IsEnabled { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<ApplicationUser, UserForListVm>()
            .ReverseMap();
    }
}