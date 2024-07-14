using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Kit;

public record KitVm : IMapFrom<Domain.Models.Kit>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public bool ShouldBeShown { get; set; }

    public decimal Price { get; set; }

    public required ImageVm Image { get; set; }
    
    public int ElementCount { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm=>vm.ElementCount, opt => opt.MapFrom(ent=>ent.Elements.Count))
            .ReverseMap()
            .ForMember(ent => ent.ImageId, opt => opt.MapFrom(vm => vm.Image.Id));
    }
}