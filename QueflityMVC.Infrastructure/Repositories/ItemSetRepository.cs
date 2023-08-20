using Microsoft.EntityFrameworkCore;
using QueflityMVC.Domain.Interfaces;
using QueflityMVC.Domain.Models;
using QueflityMVC.Infrastructure.Common;

namespace QueflityMVC.Infrastructure.Repositories
{
    public class ItemSetRepository : BaseRepository<ItemSet>, IItemSetRepository
    {
        public ItemSetRepository(Context dbContext) : base(dbContext) { }

        public ItemSet? GetFullItemSetWithMembershipsById(int id)
        {
            return _dbContext.Set<ItemSet>().Include(x=>x.SetMemberships).Include(z=> z.Image).FirstOrDefault(y=> y.Id == id);
        }
    }
}
