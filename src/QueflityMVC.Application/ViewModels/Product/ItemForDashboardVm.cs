using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Category;
using QueflityMVC.Application.ViewModels.Component;

namespace QueflityMVC.Application.ViewModels.Product;

public record ItemForCardVm : ProductForCardVm, IMapFrom<Domain.Models.Item>
{
    public required CategoryVm Category { get; set; }

    public required List<ComponentForListVm> Components { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, ItemForCardVm>()
            .IncludeBase<Domain.Models.Product, ProductForCardVm>()
            .ForMember(vm => vm.Category, opt => opt.MapFrom(it => it.Category))
            .ForMember(vm => vm.Components, opt => opt.MapFrom(it => it.Components));
    }
}