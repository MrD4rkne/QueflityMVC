using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemForSelectionVm : IMapFrom<Domain.Models.Item>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public bool IsSelected { get; set; }

    public ImageForListVm? Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemForSelectionVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ReverseMap();
    }
}