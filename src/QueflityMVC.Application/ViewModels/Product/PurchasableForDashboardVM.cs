#region

using QueflityMVC.Application.Mapping;
using QueflityMVC.Application.ViewModels.Image;

#endregion

namespace QueflityMVC.Application.ViewModels.Product;

public record ProductForCardVm : IMapFrom<Domain.Models.Product>
{
    public required int Id { get; set; }

    public required string Name { get; set; }

    public required decimal Price { get; set; }

    public required uint? OrderNo { get; set; }

    public required ImageForListVm Image { get; set; }

    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Product, ProductForCardVm>()
            .Include<Domain.Models.Kit, KitForCardVm>()
            .Include<Domain.Models.Item, ItemForCardVm>();
    }
}