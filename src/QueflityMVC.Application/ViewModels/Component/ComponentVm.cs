using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Component;

public record ComponentVm : IMapFrom<Domain.Models.Component>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Component, ComponentVm>().ReverseMap();
    }
}