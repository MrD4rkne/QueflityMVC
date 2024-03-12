using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Item;
using QueflityMVC.Application.ViewModels.Kit;

namespace QueflityMVC.Application.ViewModels.Element;

public record ElementVm : IMapFrom<Domain.Models.Element>
{
    public int Id { get; set; }

    public required ItemVm Item { get; set; }

    public required KitDetailsVm KitDetailsVm { get; set; }

    public required uint ItemsAmmount { get; set; }

    public required decimal PricePerItem { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Element, ElementVm>()
            .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
            .ForMember(vm => vm.KitDetailsVm, opt => opt.MapFrom(et => et.Kit))
            .ReverseMap()
            .ForMember(ent => ent.KitId, opt => opt.MapFrom(vm => vm.KitDetailsVm.Id))
            .ForMember(ent => ent.ItemId, opt => opt.MapFrom(vm => vm.Item.Id));
    }
}