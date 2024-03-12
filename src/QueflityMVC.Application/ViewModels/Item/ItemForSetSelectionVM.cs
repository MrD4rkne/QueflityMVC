using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Item;

public record ItemForSetSelectionVm : IMapFrom<Domain.Models.Item>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public bool IsSelected { get; set; }

    public uint Quantity { get; set; }

    public decimal Price { get; set; }

    public ImageForListVm? Image { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemForSetSelectionVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ReverseMap();
    }
}