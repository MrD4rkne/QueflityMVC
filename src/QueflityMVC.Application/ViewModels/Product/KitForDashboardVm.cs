using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Element;

namespace QueflityMVC.Application.ViewModels.Product;

public record KitForDashboardVm : ProductForDashboardVm, IMapFrom<Domain.Models.Kit>
{
    public string? Description { get; set; }

    public required List<ElementForListVm> Elements { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Kit, KitForDashboardVm>()
            .IncludeBase<Domain.Models.Product, ProductForDashboardVm>()
            .ForMember(vm => vm.Elements, opt => opt.MapFrom(kit => kit.Elements));
    }
}