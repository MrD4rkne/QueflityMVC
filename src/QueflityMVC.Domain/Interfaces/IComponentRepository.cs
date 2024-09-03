using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IComponentRepository : IBaseRepository<Component>
{
    IQueryable<Component> GetComponentsForItem(int itemId);

    IQueryable<Component> GetComponentsForPagination(int? itemId, string? nameFilter);

    Task<bool> DoesComponentWithNameExistAsync(string name);
}