using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Product;

public class ProductVm : IMapFrom<Domain.Models.Item>, IMapFrom<Domain.Models.Kit>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public uint? OrderNo { get; set; }

    public ImageForListVm Image { get; set; }

    public ProductType Type { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, ProductVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => ProductType.Item))
            .ReverseMap();

        profile.CreateMap<Domain.Models.Kit, ProductVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => ProductType.Kit))
            .ReverseMap();

        profile.CreateMap<ProductVm, Domain.Models.Product>()
            .ConstructUsing((vm, ctx) => vm.Type switch
            {
                ProductType.Item => (Domain.Models.Item)ctx.Mapper.Map(vm, typeof(ProductVm),
                    typeof(Domain.Models.Item)),
                ProductType.Kit => (Domain.Models.Kit)ctx.Mapper.Map(vm, typeof(ProductVm),
                    typeof(Domain.Models.Kit)),
                _ => throw new InvalidOperationException("Unknown purchasable type")
            });
    }
}