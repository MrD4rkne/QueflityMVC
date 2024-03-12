using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Component;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public record ItemForDashboardVm : PurchasableForDashboardVm, IMapFrom<Domain.Models.Item>
{
    public required CategoryVm Category { get; init; }

    public required List<ComponentForListVm> Components { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemForDashboardVm>()
            .IncludeBase<BasePurchasableEntity, PurchasableForDashboardVm>()
            .ForMember(vm => vm.Category, opt => opt.MapFrom(it => it.Category))
            .ForMember(vm => vm.Components, opt => opt.MapFrom(it => it.Components));
    }
}