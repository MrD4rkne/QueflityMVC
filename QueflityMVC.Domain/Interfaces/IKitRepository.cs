using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IKitRepository : IBaseRepository<Kit>
{
    Task<Kit?> GetFullKitWithMembershipsByIdAsync(int id);

    IQueryable<Kit> GetFilteredByName(string? searchName);

    Task<IQueryable<int>> GetComponenetsIdsForSet(int setId);

    IQueryable<Element> GetKitComponents(int setId);

    Task AddComponentAsync(Element componentToCreate);

    Task UpdateKitPriceAsync(int kitId);

    Task<Element?> GetElementAsync(int setId, int itemId);

    Task UpdateElementAsync(Element componentToEdit);

    Task DeleteElementAsync(int kitId, int itemId);
}