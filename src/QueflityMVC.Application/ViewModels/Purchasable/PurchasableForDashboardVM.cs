using AutoMapper;
using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public record PurchasableForDashboardVm : IMapFrom<BasePurchasableEntity>
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required decimal Price { get; init; }

    public required uint? OrderNo { get; init; }

    public required ImageForListVm Image { get; init; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<BasePurchasableEntity, PurchasableForDashboardVm>()
            .Include<Domain.Models.Kit, KitForDashboardVm>()
            .Include<Domain.Models.Item, ItemForDashboardVm>();
    }
}