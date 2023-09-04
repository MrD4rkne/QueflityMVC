using QueflityMVC.Domain.Common;
using QueflityMVC.Domain.Models;

namespace QueflityMVC.Domain.Interfaces
{
    public interface IItemSetRepository : IBaseRepository<ItemSet>
    {
        public ItemSet? GetFullItemSetWithMembershipsById(int id);

        public IQueryable<ItemSet> GetFilteredByName(string? searchName);
    }
}
