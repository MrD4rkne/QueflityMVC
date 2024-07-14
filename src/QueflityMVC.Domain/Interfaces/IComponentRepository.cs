using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IComponentRepository : IBaseRepository<Component>
{
    public IQueryable<Component> GetComponentsForItem(int itemId);

    public IQueryable<Component> GetComponentsForPagination(int? itemId, string? nameFilter);
}