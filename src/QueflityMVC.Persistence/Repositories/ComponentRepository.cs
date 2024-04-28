using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Persistence.Common;

namespace QueflityMVC.Persistence.Repositories;

public class ComponentRepository(Context dbContext) : BaseRepository<Component>(dbContext), IComponentRepository
{
    public IQueryable<Component> GetComponentsForItem(int itemId)
    {
        return GetAll().Where(x => x.Items!.Any(x => x.Id == itemId));
    }

    public IQueryable<Component> GetComponentsForPagination(int? itemId, string? nameFilter)
    {
        var matchingComponents = GetAll();

        if (itemId.HasValue) matchingComponents = matchingComponents.Where(x => x.Items!.Any(y => y.Id == itemId));
        if (!string.IsNullOrEmpty(nameFilter))
            matchingComponents = matchingComponents.Where(x => x.Name.StartsWith(nameFilter));

        return matchingComponents;
    }
}