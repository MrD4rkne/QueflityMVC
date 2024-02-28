using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueflityMVC.Domain.Models;
using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Application.ViewModels.Kit;
using QueflityMVC.Domain.Common;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace QueflityMVC.Application.ViewModels.Purchasable;
public class PurchasableVM : IMapFrom<Domain.Models.Item>, IMapFrom<Domain.Models.Kit>
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public required uint? OrderNo { get; init; }

    public required ImageForListVM Image { get; init; }

    public required PurchasableType Type { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Models.Item, PurchasableVM>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Item))
            .ReverseMap();

        profile.CreateMap<Domain.Models.Kit, PurchasableVM>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Kit))
            .ReverseMap();

        profile.CreateMap<PurchasableVM,BasePurchasableEntity>()
            .ConstructUsing((vm,ctx) => vm.Type switch
            {
                PurchasableType.Item => (Domain.Models.Item)ctx.Mapper.Map(vm, typeof(PurchasableVM), typeof(Domain.Models.Item)),
                PurchasableType.Kit => (Domain.Models.Kit)ctx.Mapper.Map(vm, typeof(PurchasableVM), typeof(Domain.Models.Kit)),
                _ => throw new InvalidOperationException("Unknown purchasable type")
            });
    }
}

public class PurchasableToTypeConvert : IValueConverter<BasePurchasableEntity, PurchasableType>
{
    public PurchasableType Convert(BasePurchasableEntity sourceMember, ResolutionContext context) { 
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
