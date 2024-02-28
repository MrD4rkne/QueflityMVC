using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces;

public interface IKitRepository : IBaseRepository<Kit>
{
    Task<Kit?> GetFullKitWithMembershipsByIdAsync(int id);

    IQueryable<Kit> GetFilteredKits(string? searchName, int? itemId);

    Task<IQueryable<int>> GetComponenetsIdsForSet(int kitId);

    IQueryable<Element> GetKitComponents(int kitId);

    Task AddComponentAsync(Element componentToCreate);

    Task UpdateKitPriceAsync(int kitId);

    Task<Element?> GetElementAsync(int kitId, int itemId);

    Task UpdateElementAsync(Element componentToEdit);

    Task DeleteElementAsync(int kitId, int itemId);
}