using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemForListVm : IMapFrom<Domain.Models.Item>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required bool ShouldBeShown { get; set; }

    public ImageForListVm? Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemForListVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image));
    }
}