using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Item;

namespace QueflityMVC.Application.ViewModels.Element;

public record ElementForListVm : IMapFrom<Domain.Models.Element>
{
    public required int Id { get; set; }

    public required ItemVm Item { get; set; }

    public required uint ItemsAmount { get; set; }

    public required decimal PricePerItem { get; set; }

    public required int KitId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Element, ElementForListVm>()
            .ForMember(vm => vm.Item, opt => opt.MapFrom(sm => sm.Item))
            .ReverseMap();
    }
}