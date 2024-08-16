using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Product;

public class ProductShortVm : IMapFrom<Domain.Models.Product>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public ImageVm Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Product, ProductShortVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(p => p.Image))
            .ReverseMap();
    }
}