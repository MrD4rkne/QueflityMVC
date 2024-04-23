using System.ComponentModel;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemVm : IMapFrom<Domain.Models.Item>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public bool ShouldBeShown { get; set; }

    public ImageVm Image { get; set; }

    [DisplayName("Category")] public int? CategoryId { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.CategoryId, opt => opt.MapFrom(ent => ent.CategoryId))
            .ReverseMap()
            .ForMember(ent => ent.ImageId, opt => opt.MapFrom(vm => vm.Image.Id));
    }
}