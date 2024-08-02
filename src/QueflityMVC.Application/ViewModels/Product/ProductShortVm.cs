using QueflityMVC.Application.Mapping;

namespace QueflityMVC.Application.ViewModels.Product;

public class ProductShortVm : IMapFrom<Domain.Models.Product>
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public void Mapping(MappingProfile profile)
    {
        profile.CreateMap<Domain.Models.Product, ProductShortVm>()
            .ReverseMap();
    }
}