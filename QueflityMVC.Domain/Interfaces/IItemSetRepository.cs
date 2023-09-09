using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IItemSetRepository : IBaseRepository<ItemSet>
    {
        ItemSet? GetFullItemSetWithMembershipsById(int id);

        IQueryable<ItemSet> GetFilteredByName(string? searchName);
        IQueryable<int> GetComponenetsIdsForSet(int setId);

        IQueryable<SetElement> GetSetComponents(int setId);
    }
}
