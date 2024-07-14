using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit;

public record KitDetailsVm : IMapFrom<Domain.Models.Kit>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public bool ShouldBeShown { get; set; }

    public required ImageVm Image { get; set; }

    public required List<ElementForListVm> ItemMemberships { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitDetailsVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.ItemMemberships, opt => opt.MapFrom(ent => ent.Elements))
            .ReverseMap()
            .ForMember(ent => ent.ImageId, opt => opt.MapFrom(vm => vm.Image.Id));
    }
}