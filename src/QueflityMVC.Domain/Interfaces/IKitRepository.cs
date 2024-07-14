using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IKitRepository : IBasePurchasableRepository<Kit>
{
    Task<Kit?> GetFullKitWithMembershipsByIdAsync(int id);

    IQueryable<Kit> GetFilteredKits(string? searchName, int? itemId);

    Task<IQueryable<int>> GetComponentsIdsForSet(int kitId);

    IQueryable<Element> GetKitComponents(int kitId);

    Task AddComponentAsync(Element componentToCreate);

    Task<Element?> GetElementAsync(int kitId, int itemId);

    Task UpdateElementAsync(Element componentToEdit);

    Task DeleteElementAsync(int kitId, int itemId);
    
    Task<int> GetElementCount(int kitId);
}