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
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Item));

        profile.CreateMap<Domain.Models.Kit, PurchasableVM>()
            .ForMember(vm => vm.Image, opt => opt.MapFrom(ent => ent.Image))
            .ForMember(vm => vm.Type, opt => opt.MapFrom(kit => PurchasableType.Kit));
    }
}
