using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Component;

public record ComponentForSelection : IMapFrom<Domain.Models.Component>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public bool IsSelected { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Component, ComponentForSelection>()
            .ReverseMap();
    }
}