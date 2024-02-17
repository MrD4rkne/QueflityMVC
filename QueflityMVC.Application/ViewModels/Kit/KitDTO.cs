using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit;

public record KitVM : IMapFrom<Domain.Models.Kit>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool ShouldBeShown { get; set; }

    public required ImageVM Image { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitVM>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ReverseMap();
    }
}