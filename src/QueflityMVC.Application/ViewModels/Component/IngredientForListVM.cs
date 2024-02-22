using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Component;

public record ComponentForListVM : IMapFrom<Domain.Models.Component>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Component, ComponentForListVM>();
    }
}