using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit;

public record KitForListVm : IMapFrom<Domain.Models.Item>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public bool ShouldBeShown { get; set; }

    public ImageForListVm? Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitForListVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image));
    }
}