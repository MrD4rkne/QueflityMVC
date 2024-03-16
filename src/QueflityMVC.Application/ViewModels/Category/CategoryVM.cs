using AutoMapper;
using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Category;

public record CategoryVm : IMapFrom<Domain.Models.Category>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Category, CategoryVm>().ReverseMap();
    }
}