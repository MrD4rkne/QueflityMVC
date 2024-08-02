using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

namespace QueflityMVC.Application.ViewModels.Product;

public record ProductForDashboardVm : IMapFrom<Domain.Models.Product>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public required uint? OrderNo { get; set; }

    public required ImageForListVm Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Product, ProductForDashboardVm>()
            .Include<Domain.Models.Kit, KitForDashboardVm>()
            .Include<Domain.Models.Item, ItemForDashboardVm>();
    }
}