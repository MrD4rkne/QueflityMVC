using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public class PurchasableVm : IMapFrom<Domain.Models.Item>, IMapFrom<Domain.Models.Kit>
{
    public int Id { get; init; }

    public string Name { get; init; }

    public decimal Price { get; init; }

    public uint? OrderNo { get; init; }

    public ImageForListVm Image { get; init; }

    public PurchasableType Type { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Item, PurchasableVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Item))
            .ReverseMap();

        profile.CreateMap<Domain.Models.Kit, PurchasableVm>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Kit))
            .ReverseMap();

        profile.CreateMap<PurchasableVm, BasePurchasableEntity>()
            .ConstructUsing((vm, ctx) => vm.Type switch
            {
                PurchasableType.Item => (Domain.Models.Item)ctx.Mapper.Map(vm, typeof(PurchasableVm), typeof(Domain.Models.Item)),
                PurchasableType.Kit => (Domain.Models.Kit)ctx.Mapper.Map(vm, typeof(PurchasableVm), typeof(Domain.Models.Kit)),
                _ => throw new InvalidOperationException("Unknown purchasable type")
            });
    }
}

public class PurchasableToTypeConvert : IValueConverter<BasePurchasableEntity, PurchasableType>
{
    public PurchasableType Convert(BasePurchasableEntity sourceMember, ResolutionContext context)
    {
        switch (sourceMember)
        {
            case Domain.Models.Item _:
                return PurchasableType.Item;

            case Domain.Models.Kit _:
                return PurchasableType.Kit;

            default:
                throw new InvalidOperationException("Unknown purchasable type");
        }
    }
}