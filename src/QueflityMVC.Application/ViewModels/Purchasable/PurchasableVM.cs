using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public class PurchasableVm : IMapFrom<Domain.Models.Item>, IMapFrom<Domain.Models.Kit>
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public uint? OrderNo { get; set; }

    public ImageForListVm Image { get; set; }

    public PurchasableType Type { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Item, PurchasableVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Item))
            .ReverseMap();

        profile.CreateMap<Domain.Models.Kit, PurchasableVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Kit))
            .ReverseMap();

        profile.CreateMap<PurchasableVm, Product>()
            .ConstructUsing((vm, ctx) => vm.Type switch
            {
                PurchasableType.Item => (Domain.Models.Item)ctx.Mapper.Map(vm, typeof(PurchasableVm),
                    typeof(Domain.Models.Item)),
                PurchasableType.Kit => (Domain.Models.Kit)ctx.Mapper.Map(vm, typeof(PurchasableVm),
                    typeof(Domain.Models.Kit)),
                _ => throw new InvalidOperationException("Unknown purchasable type")
            });
    }
}