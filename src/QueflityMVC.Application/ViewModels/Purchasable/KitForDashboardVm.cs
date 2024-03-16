using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Element;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public record KitForDashboardVm : PurchasableForDashboardVm, IMapFrom<Domain.Models.Kit>
{
    public string? Description { get; set; }

    public required List<ElementForListVm> Elements { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitForDashboardVm>()
            .IncludeBase<BasePurchasableEntity, PurchasableForDashboardVm>()
            .ForMember(vm => vm.Elements, opt => opt.MapFrom(kit => kit.Elements));
    }
}