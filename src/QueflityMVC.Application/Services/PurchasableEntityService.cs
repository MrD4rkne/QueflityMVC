using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QueflityMVC.Application.Interfaces;
using QueflityMVC.Application.ViewModels.Purchasable;
using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Interfaces;

namespace QueflityMVC.Application.Services;
public class PurchasableEntityService : IPurchasableEntityService
{
    private readonly IMapper _mapper;
    private readonly IKitRepository _kitRepository;
    private readonly IItemRepository _itemRepository;

    public PurchasableEntityService(IMapper mapper, IKitRepository kitRepository, IItemRepository itemRepository)
    {
        _mapper = mapper;
        _kitRepository = kitRepository;
        _itemRepository = itemRepository;
    }

    private IPurchasableRepository GetRepository(PurchasableType type) => type switch
    {
        PurchasableType.Kit => _kitRepository,
        PurchasableType.Item => _itemRepository,
        _ => throw new NotImplementedException()
    };

    public async Task<List<PurchasableVM>> GetEnitiesOrderVM()
    {
        var purchasableTypes = Enum.GetValues<PurchasableType>();
        if (purchasableTypes.Length == 0)
        {
            throw new InvalidOperationException("No purchasable types found");
        }

        List<PurchasableVM> results = [];
        for (int i = 0; i < purchasableTypes.Length; i++)
        {
            var entities = await GetRepository(purchasableTypes[i]).GetVisibileEntities()
                .ProjectTo<PurchasableVM>(_mapper.ConfigurationProvider)
                .ToListAsync();
            results.AddRange(entities);
        }
        return results;
    }
}
