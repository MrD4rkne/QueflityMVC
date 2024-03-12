using AutoMapper;
using QueflityMVC.Domain.Common;

namespace QueflityMVC.Application.ViewModels.Purchasable;

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