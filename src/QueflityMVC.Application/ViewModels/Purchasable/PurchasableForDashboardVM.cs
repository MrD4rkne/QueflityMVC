using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.ViewModels.Purchasable;

public record PurchasableForDashboardVm : IMapFrom<Product>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public required uint? OrderNo { get; set; }

    public required ImageForListVm Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Product, PurchasableForDashboardVm>()
            .Include<Domain.Models.Kit, KitForDashboardVm>()
            .Include<Domain.Models.Item, ItemForDashboardVm>();
    }
}