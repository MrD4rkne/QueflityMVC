using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemSetRepository : BaseRepository<ItemSet>, IItemSetRepository
    {
        public ItemSetRepository(Context dbContext) : base(dbContext) { }

        public IQueryable<ItemSet> GetFilteredByName(string? searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return GetAll();
            }

            return GetAll().Where(x => x.Name.Contains(searchName));
        }

        public ItemSet? GetFullItemSetWithMembershipsById(int id)
        {
            return _dbContext.Set<ItemSet>().Include(x => x.Elements).Include(z => z.Image).FirstOrDefault(y => y.Id == id);
        }
    }
}
