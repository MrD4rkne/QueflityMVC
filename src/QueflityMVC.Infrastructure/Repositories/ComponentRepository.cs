using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories;

public class ComponentRepository : BaseRepository<Component>, IComponentRepository
{
    public ComponentRepository(Context dbContext) : base(dbContext)
    {
    }

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