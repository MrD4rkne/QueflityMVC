using QueflityMVC.Application.Mapping;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Application.ViewModels.Other;

public record RoleForSelectionVm : IMapFrom<ApplicationRole>
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    public bool IsSelected { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<ApplicationRole, RoleForSelectionVm>()
            .ReverseMap();
    }
}