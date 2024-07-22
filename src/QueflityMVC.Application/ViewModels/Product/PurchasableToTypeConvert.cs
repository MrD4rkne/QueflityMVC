using AutoMapper;

namespace QueflityMVC.Application.ViewModels.Product;

public class ProductToTypeConvert : IValueConverter<Domain.Common.Product, ProductType>
{
    public ProductType Convert(Domain.Common.Product sourceMember, ResolutionContext context)
    {
        switch (sourceMember)
        {
            case Domain.Models.Item _:
                return ProductType.Item;

            case Domain.Models.Kit _:
                return ProductType.Kit;

            default:
                throw new InvalidOperationException("Unknown purchasable type");
        }
    }
}