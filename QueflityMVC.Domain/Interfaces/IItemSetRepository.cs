using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IKitRepository : IBaseRepository<Kit>
    {
        Kit? GetFullKitWithMembershipsById(int id);

        IQueryable<Kit> GetFilteredByName(string? searchName);

        IQueryable<int> GetComponenetsIdsForSet(int setId);

        IQueryable<Element> GetSetComponents(int setId);

        void AddComponent(Element componentToCreate);

        void UpdateKitPrice(int kitId);

        Element? GetElement(int setId, int itemId);
        void UpdateComponent(Element componentToEdit);

        void DeleteElement(int kitId, int itemId);
    }
}
